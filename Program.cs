using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineJobPortal.Data;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Repositories;
using OnlineJobPortal.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//  Add services 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


//  Swagger with JWT 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "OnlineJobPortal API", Version = "v1" });

    // JWT Bearer token support
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOi...\"",
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

//  Database Config 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//  Repositories 
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();


//Services 
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IRecruiterService, RecruiterService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IResumeService, ResumeService>();


//  JWT Authentication 
System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var keyString = jwtSettings["Key"];
if (string.IsNullOrEmpty(keyString))
{
    throw new InvalidOperationException("JWT key not found in configuration");
}
var key = Encoding.UTF8.GetBytes(keyString);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

//  Authorization Policies 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("RecruiterPolicy", policy =>
    {
        policy.RequireRole("Recruiter");
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("JobSeekerPolicy", policy =>
    {
        policy.RequireRole("JobSeeker");
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "role" && c.Value == "Admin"));
    });
});

var app = builder.Build();

//  Middleware 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
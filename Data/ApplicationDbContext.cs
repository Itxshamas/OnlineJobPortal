
using Microsoft.EntityFrameworkCore;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<JobPost> JobPosts{ get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AdminLog> AdminLogs { get; set; }
        public DbSet<Resume> Resumes { get; set; }
    }
}
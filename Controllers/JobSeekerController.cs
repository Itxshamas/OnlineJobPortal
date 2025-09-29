using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.DTOs;
using OnlineJobPortal.IServices;
using System.Security.Claims;
using System.IO;
using System.Threading.Tasks;

namespace OnlineJobPortal.Controllers
{
    [Authorize(Roles = "JobSeeker")]
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IResumeService _resumeService;

        public JobSeekerController(IUserService userService, IResumeService resumeService)
        {
            _userService = userService;
            _resumeService = resumeService;
        }

        // GET: api/JobSeeker/profile
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            Models.ApplicationUser user = _userService.GetUserByEmail(email);
            if (user == null)
                return NotFound();

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.isActive,
                CreatedAt = user.CreatedAt
            };

            return Ok(userDto);
        }

        // PUT: api/JobSeeker/profile
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDto userDto)
        {
            string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            Models.ApplicationUser user = _userService.GetUserByEmail(email);

            if (user == null)
                return NotFound();

            if (user.Id != userDto.Id)
                return Forbid();

            await _userService.UpdateUserAsync(user.Id, userDto);

            return NoContent();
        }

        //Get Resume 

        [HttpGet("resume")]
        public async Task<IActionResult> GetResume()
        {
            string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            Models.ApplicationUser user = _userService.GetUserByEmail(email);
            if (user == null)
                return NotFound(new { message = "User not found" });

            ResumeDto resumeDto = await _resumeService.GetResumeByUserIdAsync(user.Id);
            if (resumeDto == null)
                return NotFound(new { message = "Resume not found for this user" });

            string filePath = resumeDto.FilePath;
            if (!System.IO.File.Exists(filePath))
                return NotFound(new { message = "Resume file not found on server" });

            MemoryStream memory = new MemoryStream();
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "application/octet-stream", resumeDto.FileName);
        }


        // POST: api/JobSeeker/resume
        [HttpPost("upload-resume")]
        public async Task<IActionResult> UploadResume([FromBody] string fileName)
        {
            string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            Models.ApplicationUser user = _userService.GetUserByEmail(email);
            if (user == null)
                return NotFound();

            bool result = await _resumeService.UploadResumeAsync(user.Id, fileName);
            if (!result)
                return BadRequest("Resume already exists.");

            return Ok();
        }

        // PUT: api/JobSeeker/resume
        [HttpPut("resume")]
        public async Task<IActionResult> UpdateResume([FromBody] string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest("File name not provided");

            string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            Models.ApplicationUser user = _userService.GetUserByEmail(email);

            if (user == null)
                return NotFound();

            bool result = await _resumeService.UpdateResumeAsync(user.Id, fileName);

            if (!result)
                return NotFound("No resume found to update.");

            return Ok();
        }
    }
}

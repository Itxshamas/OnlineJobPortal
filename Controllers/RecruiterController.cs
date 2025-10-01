using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.DTOs;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;
using System.Security.Claims;

namespace OnlineJobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Recruiter")]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;
        private readonly IJobService _jobService;

        public RecruiterController(IRecruiterService recruiterService, IJobService jobService)
        {
            _recruiterService = recruiterService;
            _jobService = jobService;
        }

        // Get all recruiters
        [HttpGet("GetAllRecruiters")]
        [AllowAnonymous]
        public IActionResult GetAllRecruiters()
        {
            IEnumerable<ApplicationUserDto> recruiters = _recruiterService.GetAll();
            return Ok(recruiters);
        }

        // Get recruiter profile by id
        [HttpGet("{id}")]
        public IActionResult GetRecruiterProfile(int id)
        {
            ApplicationUser recruiter = _recruiterService.GetById(id);
            if (recruiter == null)
            {
                return NotFound();
            }
            return Ok(recruiter);
        }

        // Update recruiter profile by id
        [HttpPut("{id}")]
        public IActionResult UpdateRecruiterProfile(int id, [FromBody] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _recruiterService.Update(id, user);
            return NoContent();
        }

        //Fetch logged-in recruiter profile (uses claims safely)
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            int recruiterId = GetRecruiterIdFromClaims();
            if (recruiterId == 0)
                return Unauthorized();

            ApplicationUser user = _recruiterService.GetProfile(recruiterId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        // Update logged-in recruiter profile
        [HttpPut("profile")]
        public IActionResult UpdateProfile([FromBody] ApplicationUser user)
        {
            int recruiterId = GetRecruiterIdFromClaims();
            if (recruiterId == 0) return Unauthorized();

            user.Id = recruiterId;
            _recruiterService.UpdateRecProfile(recruiterId, user);

            return NoContent();
        }

        // Get all job posts of logged-in recruiter
        [HttpGet("jobposts")]
        public IActionResult GetAllJobPosts()
        {
            int recruiterId = GetRecruiterIdFromClaims();
            if (recruiterId == 0) return Unauthorized();

            IEnumerable<JobPostDto> jobs = _jobService.GetAllJobs()
                                                     .Where(j => j.RecruiterId == recruiterId);
            return Ok(jobs);
        }

        // Create new job post
        [HttpPost("jobposts")]
        public IActionResult CreateJobPost([FromBody] JobPostDto jobPostDto)
        {
            int recruiterId = GetRecruiterIdFromClaims();
            if (recruiterId == 0) return Unauthorized();

            jobPostDto.RecruiterId = recruiterId;
            _jobService.CreateJob(jobPostDto);

            return Ok(jobPostDto);
        }

        // Get job post by ID (only if owned by recruiter)
        [HttpGet("jobposts/{id}")]
        public IActionResult GetJobPostById([FromRoute] int id)
        {
            int recruiterId = GetRecruiterIdFromClaims();
            if (recruiterId == 0) return Unauthorized();

            JobPostDto job = _jobService.GetJobById(id);
            if (job == null || job.RecruiterId != recruiterId)
                return NotFound();

            return Ok(job);
        }

        // Update job post (only owner)
        [HttpPut("jobposts/{id}")]
        public IActionResult UpdateJobPost(int id, [FromBody] JobPostDto jobPostDto)
        {
            int recruiterId = GetRecruiterIdFromClaims();
            if (recruiterId == 0) return Unauthorized();

            JobPostDto existingJob = _jobService.GetJobById(id);
            if (existingJob == null || existingJob.RecruiterId != recruiterId)
                return NotFound();

            jobPostDto.RecruiterId = recruiterId;
            _jobService.UpdateJob(id, jobPostDto);

            return NoContent();
        }

        //  Delete job post (only owner)
        [HttpDelete("jobposts/{id}")]
        public IActionResult DeleteJobPost(int id)
        {
            int recruiterId = GetRecruiterIdFromClaims();
            if (recruiterId == 0) return Unauthorized();

            JobPostDto existingJob = _jobService.GetJobById(id);
            if (existingJob == null || existingJob.RecruiterId != recruiterId)
                return NotFound();

            _jobService.DeleteJob(id);

            return NoContent();
        }

        //  Helper method to safely extract recruiterId from claims
        private int GetRecruiterIdFromClaims()
        {
            string? claimValue = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(claimValue))
            {
                claimValue = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                             ?? User.FindFirst("sub")?.Value;
            }

            return int.TryParse(claimValue, out int recruiterId) ? recruiterId : 0;
        }

    }
}




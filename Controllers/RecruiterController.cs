using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.DTOs;
using OnlineJobPortal.IServices;
using System.Security.Claims;


namespace OnlineJobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RecruiterPolicy")]

    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;
        private readonly IJobService _jobService;


        public RecruiterController(IRecruiterService recruiterService, IJobService jobService)
        {
            _recruiterService = recruiterService;
            _jobService = jobService;

        }
        //Get All recuriter
        [HttpGet("GetAllRecruiters")]
        public IActionResult GetAllRecruiters()
        {
            IEnumerable<RecruiterDto> recruiters = _recruiterService.GetAll();
            return Ok(recruiters);
        }
        //Recruiter Profile
        [HttpGet("{id}")]
        public IActionResult GetRecruiterProfile(int id)
        {
            RecruiterDto recruiter = _recruiterService.GetById(id);
            if (recruiter == null)
            {
                return NotFound();
            }
            return Ok(recruiter);
        }
        //Update Recruiter Profile
        [HttpPut("{id}")]
        public IActionResult UpdateRecruiterProfile(int id, [FromBody] RecruiterDto recruiterDto)
        {
            if (id != recruiterDto.Id)
            {
                return BadRequest();
            }

            _recruiterService.Update(id, recruiterDto);
            return NoContent();
        }
        // Fetch logged-in recruiter profile 
        [Authorize(Roles = "Recruiter")]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            string? claimValue = User.FindFirst("recruiterId")?.Value
                                  ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int recruiterId))
                return Unauthorized();
            RecruiterDto recruiterDto = _recruiterService.GetProfile(recruiterId);

            if (recruiterDto == null)
                return NotFound();

            return Ok(recruiterDto);
        }
        // Update logged-in recruiter profile
        [Authorize(Roles = "Recruiter")]
        [HttpPut("profile")]
        public IActionResult UpdateProfile([FromBody] RecruiterDto recruiterDto)
        {
            string? claimValue = User.FindFirst("recruiterId")?.Value
                                  ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int recruiterId))
                return Unauthorized();

            _recruiterService.UpdateRecProfile(recruiterId, recruiterDto);

            return NoContent();
        }



        // Get all job posts for logged-in recruiter
        [HttpGet("jobposts")]
        public IActionResult GetAllJobPosts()
        {
            string recruiterIdClaim = User.FindFirst("recruiterId")?.Value
                                      ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(recruiterIdClaim) || !int.TryParse(recruiterIdClaim, out int recruiterId))
                return Unauthorized();

             IEnumerable<JobPostDto> jobs = _jobService.GetAllJobs()
                                  .Where(j => j.RecruiterId == recruiterId);
            return Ok(jobs);
        }

        // Create a new job post
        [HttpPost("jobposts")]
        public IActionResult CreateJobPost([FromBody] JobPostDto jobPostDto)
        {
            string recruiterIdClaim = User.FindFirst("recruiterId")?.Value
                                      ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(recruiterIdClaim) || !int.TryParse(recruiterIdClaim, out int recruiterId))
                return Unauthorized();

            jobPostDto.RecruiterId = recruiterId;
            _jobService.CreateJob(jobPostDto);

            return Ok(jobPostDto);
        }

        // Get a specific job post by ID
        [HttpGet("jobposts/{id}")]
        public IActionResult GetJobPostById(int id)
        {
            string recruiterIdClaim = User.FindFirst("recruiterId")?.Value
                                      ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(recruiterIdClaim) || !int.TryParse(recruiterIdClaim, out int recruiterId))
                return Unauthorized();

            JobPostDto job = _jobService.GetJobById(id);
            if (job == null || job.RecruiterId != recruiterId)
                return NotFound();

            return Ok(job);
        }

        // Update a job post (only by the recruiter who owns it)
        [HttpPut("jobposts/{id}")]
        public IActionResult UpdateJobPost(int id, [FromBody] JobPostDto jobPostDto)
        {
            string recruiterIdClaim = User.FindFirst("recruiterId")?.Value
                                      ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(recruiterIdClaim) || !int.TryParse(recruiterIdClaim, out int recruiterId))
                return Unauthorized();

            JobPostDto existingJob = _jobService.GetJobById(id);
            if (existingJob == null || existingJob.RecruiterId != recruiterId)
                return NotFound();

            jobPostDto.RecruiterId = recruiterId; // Ensure recruiterId matches logged-in user
            _jobService.UpdateJob(id, jobPostDto);

            return NoContent();
        }

        // Delete a job post 
        [HttpDelete("jobposts/{id}")]
        public IActionResult DeleteJobPost(int id)
        {
            string recruiterIdClaim = User.FindFirst("recruiterId")?.Value
                                      ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(recruiterIdClaim) || !int.TryParse(recruiterIdClaim, out int recruiterId))
                return Unauthorized();

            JobPostDto existingJob = _jobService.GetJobById(id);
            if (existingJob == null || existingJob.RecruiterId != recruiterId)
                return NotFound();

            _jobService.DeleteJob(id);

            return NoContent();
        }
    }
}
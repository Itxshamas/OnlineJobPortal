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

        public RecruiterController(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
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

        [HttpPost("CreateJobPost")]
        public IActionResult CreateJobPost([FromBody] JobPostDto jobPostDto)
        {
         string recruiterIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(recruiterIdClaim) || !int.TryParse(recruiterIdClaim, out int recruiterId))
            {
                return Unauthorized(new { Message = "Invalid recruiter ID in token" });
            }

            JobPostDto jobPost = _recruiterService.CreateJobPost(jobPostDto, recruiterId); 
            return Ok(jobPost);
        }
    }
}

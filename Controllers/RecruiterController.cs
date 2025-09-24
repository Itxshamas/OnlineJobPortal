using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.DTOs;
using OnlineJobPortal.IServices;

namespace OnlineJobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;

        public RecruiterController(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        [HttpGet("{id}")]
        public IActionResult GetRecruiterProfile(int id)
        {
            var recruiter = _recruiterService.GetById(id);
            if (recruiter == null)
            {
                return NotFound();
            }
            return Ok(recruiter);
        }

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
    }
}

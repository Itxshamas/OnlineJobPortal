using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.DTOs;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IRecruiterService _recruiterService;
        private readonly IAdminService _adminService;
        private readonly IApplicationService _applicationService;
        private readonly IResumeService _resumeService;

        private readonly IUserService _userService;

        public AdminController(
            ICategoryService categoryService,
            IRecruiterService recruiterService,
            IAdminService adminService,
            IApplicationService applicationService,
            IResumeService resumeService,
            IUserService userService
            )
        {
            _categoryService = categoryService;
            _recruiterService = recruiterService;
            _adminService = adminService;
            _applicationService = applicationService;
            _resumeService = resumeService;
            _userService = userService;

        }
        //Get All Logged in Admins 
        [HttpGet("GetAllAdmin")]
        public IActionResult GetAllAdmin()
        {
            IEnumerable<ApplicationUserDto> admins = _adminService.GetAllAdmins();
            return Ok(admins);
        }
        //Get All Job- Seekers :
        [HttpGet("GetAllJobSeekers")]
        public IActionResult GetAllJobSeekers()
        {
            IEnumerable<ApplicationUserDto> jobseekers = _userService.GetAllJobSeekers();
            return Ok(jobseekers);
        }

        //JobSeeker Crud :
        [HttpPost("PostJobSeeker")]
        public IActionResult AddJobSeeker([FromBody] ApplicationUser user)
        {
            _userService.AddJobSeeker(user);
            return Ok(new { Message = "JobSeeker added successfully" });
        }

        [HttpGet("GetJobSeekerById")]
        public IActionResult GetJobSeekerById(int id)
        {
            ApplicationUser jobseeker = _userService.GetJobSeeker(id);
            if (jobseeker == null)
            {
                return NotFound(new { Message = "JobSeeker not found" });
            }
            return Ok(jobseeker);
        }

        [HttpPut("UpdateJobSeekerById")]
        public IActionResult UpdateJobSeeker(int id, [FromBody] ApplicationUser user)
        {
            ApplicationUser? existing = _userService.UpadeEmployeeById(id);
            if (existing == null)
            {
                return NotFound(new { Message = "JobSeeker not found" });
            }

            _userService.Update(id, user);
            return Ok(new { Message = "JobSeeker updated successfully" });
        } 

          [HttpDelete("DeleteJobSeekersById")]
        public IActionResult DeleteJobSeekers(int id)
        {
            ApplicationUser? recruiter = _userService.DeleteById(id);
            if (recruiter == null)
            {
                return NotFound(new { Message = "Recruiter not found" });
            }

            _recruiterService.Delete(id);
            return Ok(new { Message = "Recruiter deleted successfully" });
        } 

        //Recruiter CRUD
        [HttpGet("GetAllRecruiters")]
        public IActionResult GetRecruiters()
        {
            IEnumerable<ApplicationUserDto> recruiters = _recruiterService.GetAll();
            return Ok(recruiters);
        }

        [HttpGet("GetRecruiterById")]
        public IActionResult GetRecruiter(int id)
        {
            ApplicationUser? recruiter = _recruiterService.GetById(id);
            if (recruiter == null)
            {
                return NotFound(new { Message = "Recruiter not found" });
            }
            return Ok(recruiter);
        }

        [HttpPost("PostNewRecruiter")]
        public IActionResult AddRecruiter([FromBody] ApplicationUser user)
        {
            _recruiterService.Add(user);
            return Ok(new { Message = "Recruiter added successfully" });
        }

        [HttpPut("UpdateRecruiterById")]
        public IActionResult UpdateRecruiter(int id, [FromBody] ApplicationUser user)
        {
            ApplicationUser? existing = _recruiterService.GetById(id);
            if (existing == null)
            {
                return NotFound(new { Message = "Recruiter not found" });
            }

            _recruiterService.Update(id, user);
            return Ok(new { Message = "Recruiter updated successfully" });
        }

        [HttpDelete("DeleteRecruiterById")]
        public IActionResult DeleteRecruiter(int id)
        {
            ApplicationUser? recruiter = _recruiterService.GetById(id);
            if (recruiter == null)
            {
                return NotFound(new { Message = "Recruiter not found" });
            }

            _recruiterService.Delete(id);
            return Ok(new { Message = "Recruiter deleted successfully" });
        }

        // Category CRUD
        [HttpGet("JobCategories")]
        public IActionResult GetCategories()
        {
            IEnumerable<Category> categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("JobCategories/{id}")]
        public IActionResult GetCategory(int id)
        {
            Category category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound(new { Message = "Category not found" });
            }
            return Ok(category);
        }

        [HttpPost("JobCategories")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            _categoryService.Add(category);
            return Ok(new { Message = "Category added successfully" });
        }

        [HttpPut("JobCategories/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            Category existing = _categoryService.GetById(id);
            if (existing == null)
            {
                return NotFound(new { Message = "Category not found" });
            }

            existing.Name = category.Name;
            _categoryService.Update(existing);

            return Ok(new { Message = "Category updated successfully" });
        }

        [HttpDelete("JobCategories/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Category category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound(new { Message = "Category not found" });
            }

            _categoryService.Delete(id);
            return Ok(new { Message = "Category deleted successfully" });
        }



        //  System Reports
        [HttpGet("SystemReports")]
        public IActionResult GetReports()
        {
            AdminReport report = _adminService.GetSystemReport();
            return Ok(report);
        }
        //Applications 
        [HttpGet("Applications")]
        public async Task<IActionResult> GetApplications()
        {
            IEnumerable<ApplicationDto> applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpDelete("Applications/{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            bool success = await _applicationService.DeleteApplicationAsync(id);
            if (!success)
            {
                return NotFound(new { Message = "Application not found" });
            }
            return Ok(new { Message = "Application deleted successfully" });
        }



        [HttpGet("Resumes")]
        public async Task<IActionResult> GetResumes()
        {
            IEnumerable<ResumeDto>? resumes = await _resumeService.GetAllResumesAsync();
            return Ok(resumes);
        }

        [HttpDelete("Resumes/{id}")]
        public async Task<IActionResult> DeleteResume(int id)
        {
            bool success = await _resumeService.DeleteResumeAsync(id);
            if (!success)
            {
                return NotFound(new { Message = "Resume not found" });
            }
            return Ok(new { Message = "Resume deleted successfully" });
        }

    }
}
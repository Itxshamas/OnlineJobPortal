using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;

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

        public AdminController(
            ICategoryService categoryService,
            IRecruiterService recruiterService,
            IAdminService adminService,
            IApplicationService applicationService,
            IResumeService resumeService

            )
        {
            _categoryService = categoryService;
            _recruiterService = recruiterService;
            _adminService = adminService;
            _applicationService = applicationService;
            _resumeService = resumeService;

        }


        [HttpGet("GetAllAdmin")]
        public IActionResult GetAllAdmin()
        {
            var allUsers = _adminService.GetAllUsers();
            var admins = allUsers.Where(u => u.Role == "Admin");
            return Ok(admins);

        }

        //  Recruiter CRUD
        [HttpGet("Recruiters")]
        public IActionResult GetRecruiters() => Ok(_recruiterService.GetAll());

        [HttpGet("Recruiters/{id}")]
        public IActionResult GetRecruiter(int id)
        {
            var recruiter = _recruiterService.GetById(id);
            if (recruiter == null)
                return NotFound(new { Message = "Recruiter not found" });
            return Ok(recruiter);
        }

        [HttpPost("Recruiters")]
        public IActionResult AddRecruiter([FromBody] Recruiter recruiter)
        {
            _recruiterService.Add(recruiter);
            return Ok(new { Message = "Recruiter added successfully" });
        }

        [HttpPut("Recruiters/{id}")]
        public IActionResult UpdateRecruiter(int id, [FromBody] Recruiter recruiter)
        {
            var existing = _recruiterService.GetById(id);
            if (existing == null)
                return NotFound(new { Message = "Recruiter not found" });

            existing.FullName = recruiter.FullName;
            existing.CompanyName = recruiter.CompanyName;
            existing.Email = recruiter.Email;
            existing.Phone = recruiter.Phone;
            existing.IsActive = recruiter.IsActive;

            _recruiterService.Update(existing);
            return Ok(new { Message = "Recruiter updated successfully" });
        }

        [HttpDelete("Recruiters/{id}")]
        public IActionResult DeleteRecruiter(int id)
        {
            var recruiter = _recruiterService.GetById(id);
            if (recruiter == null)
                return NotFound(new { Message = "Recruiter not found" });

            _recruiterService.Delete(id);
            return Ok(new { Message = "Recruiter deleted successfully" });
        }

        // Category CRUD
        [HttpGet("JobCategories")]
        public IActionResult GetCategories() => Ok(_categoryService.GetAll());

        [HttpGet("JobCategories/{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound(new { Message = "Category not found" });
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
            var existing = _categoryService.GetById(id);
            if (existing == null)
                return NotFound(new { Message = "Category not found" });

            existing.Name = category.Name;
            _categoryService.Update(existing);

            return Ok(new { Message = "Category updated successfully" });
        }

        [HttpDelete("JobCategories/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound(new { Message = "Category not found" });

            _categoryService.Delete(id);
            return Ok(new { Message = "Category deleted successfully" });
        }

        //  Admin Logs
        [HttpGet("AdminLogs")]
        public IActionResult GetLogs()
        {
            var logs = _adminService.GetAllLogs();
            return Ok(logs);
        }

        //  System Reports
        [HttpGet("SystemReports")]
        public IActionResult GetReports()
        {
            var report = _adminService.GetSystemReport();
            return Ok(report);
        }
        //Applications 
        [HttpGet("Applications")]
        public async Task<IActionResult> GetApplications()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpDelete("Applications/{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var success = await _applicationService.DeleteApplicationAsync(id);
            if (!success) return NotFound(new { Message = "Application not found" });
            return Ok(new { Message = "Application deleted successfully" });
        }



        [HttpGet("Resumes")]
        public async Task<IActionResult> GetResumes()
        {
            var resumes = await _resumeService.GetAllResumesAsync();
            return Ok(resumes);
        }

        [HttpDelete("Resumes/{id}")]
        public async Task<IActionResult> DeleteResume(int id)
        {
            var success = await _resumeService.DeleteResumeAsync(id);
            if (!success) return NotFound(new { Message = "Resume not found" });
            return Ok(new { Message = "Resume deleted successfully" });
        }

    }
}

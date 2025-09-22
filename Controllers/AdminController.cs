using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Interfaces.IServices;
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

        public AdminController(
            ICategoryService categoryService,
            IRecruiterService recruiterService,
            IAdminService adminService)
        {
            _categoryService = categoryService;
            _recruiterService = recruiterService;
            _adminService = adminService;
        }

        //  Recruiter CRUD 
        [HttpGet("Recruiters")]
        public IActionResult GetRecruiters()
        {
            return Ok(_recruiterService.GetAll());
        }

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

        // ================= Category CRUD =================
        [HttpGet("Categories")]
        public IActionResult GetCategories()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpGet("Categories/{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound(new { Message = "Category not found" });

            return Ok(category);
        }

        [HttpPost("Categories")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            _categoryService.Add(category);
            return Ok(new { Message = "Category added successfully" });
        }

        [HttpPut("Categories/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            var existing = _categoryService.GetById(id);
            if (existing == null)
                return NotFound(new { Message = "Category not found" });

            existing.Name = category.Name;
            _categoryService.Update(existing);

            return Ok(new { Message = "Category updated successfully" });
        }

        [HttpDelete("Categories/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return NotFound(new { Message = "Category not found" });

            _categoryService.Delete(id);
            return Ok(new { Message = "Category deleted successfully" });
        }

        // ================= Admin Logs =================
        [HttpGet("Logs")]
        public IActionResult GetLogs()
        {
            var logs = _adminService.GetAllLogs();
            return Ok(logs);
        }
    }
}

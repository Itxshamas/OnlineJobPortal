using OnlineJobPortal.Data;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineJobPortal.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AdminLog> GetAllLogs()
        {
            return _context.AdminLogs.ToList();
        }

        public void AddLog(AdminLog log)
        {
            _context.AdminLogs.Add(log);
            _context.SaveChanges();
        }

        public AdminReport GetSystemReport()
        {
            return new AdminReport
            {
                TotalUsers = _context.ApplicationUsers.Count(),
                TotalRecruiters = _context.ApplicationUsers.Count(u => u.Role == "Recruiter"),
                TotalJobPosts = _context.JobPosts.Count(),
                ActiveJobPosts = _context.JobPosts.Count(j => j.Status == "Active"),
                TotalApplications = _context.Applications.Count(),
                TotalResumes = _context.Resumes.Count()
            };
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _context.ApplicationUsers.ToList();
        }
    }
}
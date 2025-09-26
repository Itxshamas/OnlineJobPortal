using OnlineJobPortal.Interfaces;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public IEnumerable<AdminLog> GetAllLogs()
        {
            return _adminRepository.GetAllLogs();
        }

        public void AddLog(AdminLog log)
        {
            _adminRepository.AddLog(log);
        }

        public AdminReport GetSystemReport()
        {
            return _adminRepository.GetSystemReport();
        }
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _adminRepository.GetAllUsers();
        }

        public IEnumerable<ApplicationUser> GetAllAdmins()
        {
            return _adminRepository.GetAllUsers().Where(u => u.Role == "Admin");
        }
    }
}
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.Interfaces.IServices;
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
    }
}
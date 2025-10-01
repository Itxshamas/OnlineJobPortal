using OnlineJobPortal.Interfaces;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;
using OnlineJobPortal.DTOs;

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

        

        public AdminReport GetSystemReport()
        {
            return _adminRepository.GetSystemReport();
        }
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _adminRepository.GetAllUsers();
        }

        public IEnumerable<ApplicationUserDto> GetAllAdmins()
        {
            return _adminRepository.GetAllUsers().Where(u => u.Role == "Admin").Select(u=>new ApplicationUserDto
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                isActive = u.isActive,
                Phone = u.Phone,
                CompanyName = u.CompanyName,
                CreatedAt = u.CreatedAt
            });
        }
    }
}
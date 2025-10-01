using OnlineJobPortal.DTOs;
using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.IServices
{
    public interface IAdminService
    {
        AdminReport GetSystemReport();
        IEnumerable<ApplicationUser> GetAllUsers();
        IEnumerable<ApplicationUserDto> GetAllAdmins();

    }
}
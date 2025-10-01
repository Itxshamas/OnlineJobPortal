using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.Interfaces
{
    public interface IAdminRepository
    {
       
        AdminReport GetSystemReport();

        IEnumerable<ApplicationUser> GetAllUsers();
    }
}
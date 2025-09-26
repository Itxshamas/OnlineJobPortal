using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<AdminLog> GetAllLogs();
        void AddLog(AdminLog log);
        AdminReport GetSystemReport();

        IEnumerable<ApplicationUser> GetAllUsers();
    }
}
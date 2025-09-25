using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.IServices
{
    public interface IAdminService
    {
        IEnumerable<AdminLog> GetAllLogs();
        void AddLog(AdminLog log);
        AdminReport GetSystemReport();
        IEnumerable<ApplicationUser> GetAllUsers();

    }
}

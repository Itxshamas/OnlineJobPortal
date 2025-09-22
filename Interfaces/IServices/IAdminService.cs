
using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.Interfaces.IServices
{
    public interface IAdminService
    {
        IEnumerable<AdminLog> GetAllLogs();
    }
}

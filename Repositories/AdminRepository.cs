
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
    }
}

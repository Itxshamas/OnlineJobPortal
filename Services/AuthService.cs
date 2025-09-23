using OnlineJobPortal.IServices;
using OnlineJobPortal.Data;
using OnlineJobPortal.Models;
using System.Linq;

namespace OnlineJobPortal.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationUser? AuthenticateUser(string email, string password)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
        }

        public ApplicationUser? GetUserByEmail(string email)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Email == email);
        }

        public void RegisterUser(ApplicationUser user)
        {
            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
        }
    }
}

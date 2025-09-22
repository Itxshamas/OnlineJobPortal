using Microsoft.EntityFrameworkCore;
using OnlineJobPortal.Data;
using OnlineJobPortal.Models;
using OnlineJobPortal.Interfaces;

namespace OnlineJobPortal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetUsersByRole(string role)
        {
            return _context.ApplicationUsers.Where(u => u.Role == role).ToList();
        }

        public ApplicationUser? GetUserById(int id)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser? GetUserByEmail(string email)
        {
            return _context.ApplicationUsers.FirstOrDefault(u => u.Email == email);
        }

        public void AddUser(ApplicationUser user)
        {
            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(ApplicationUser user)
        {
            _context.ApplicationUsers.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.ApplicationUsers.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}

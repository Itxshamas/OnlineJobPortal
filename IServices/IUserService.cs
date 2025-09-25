using OnlineJobPortal.Models;

namespace OnlineJobPortal.IServices
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetUsersByRole(string role);
        ApplicationUser? GetUserById(int id);
        void AddUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(int id);
        ApplicationUser? GetUserByEmail(string email);
    }
}

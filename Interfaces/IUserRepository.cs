using OnlineJobPortal.Models;

namespace OnlineJobPortal.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetUsersByRole(string role);
        IEnumerable<ApplicationUser> GetJobSeekerByRole (string role);
        ApplicationUser? GetUserById(int id);
        void AddUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        void DeleteUser(int id);
        ApplicationUser? GetUserByEmail(string email);
    }
}
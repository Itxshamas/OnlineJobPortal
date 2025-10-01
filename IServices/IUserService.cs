using OnlineJobPortal.DTOs;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.IServices
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetUsersByRole(string role);
        IEnumerable<ApplicationUserDto> GetAllJobSeekers();
        ApplicationUser? GetUserById(int id);
        ApplicationUser? GetJobSeeker(int id);
        ApplicationUser? UpadeEmployeeById(int id);
        ApplicationUser DeleteById (int id);
        void AddUser(ApplicationUser user);
        void AddJobSeeker(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        Task UpdateUserAsync(int userId, UserDto userDto);
        void DeleteUser(int id);
        ApplicationUser? GetUserByEmail(string email);
        void Update(int id, ApplicationUser user);

    }
}
using OnlineJobPortal.Models;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.IServices;

namespace OnlineJobPortal.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<ApplicationUser> GetUsersByRole(string role)
        {
            return _userRepository.GetUsersByRole(role);
        }

        public ApplicationUser? GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public ApplicationUser? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public void AddUser(ApplicationUser user)
        {
            _userRepository.AddUser(user);
        }

        public void UpdateUser(ApplicationUser user)
        {
            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}

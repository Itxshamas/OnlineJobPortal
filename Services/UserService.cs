using OnlineJobPortal.DTOs;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;

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

        public async Task UpdateUserAsync(int userId, UserDto userDto)
        {
            ApplicationUser user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.FullName = userDto.FullName;
                user.Email = userDto.Email;
                await _userRepository.UpdateUserAsync(user);
            }
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}
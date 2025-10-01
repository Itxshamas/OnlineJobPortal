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

        public IEnumerable<ApplicationUserDto> GetAllJobSeekers()
        {
            return _userRepository.GetJobSeekerByRole("JobSeeker")
                .Select(u => new ApplicationUserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    isActive = u.isActive,
                    Phone = u.Phone,
                    CompanyName = u.CompanyName,
                    CreatedAt = u.CreatedAt
                })
                .ToList();
        }

        //JobSeekers 
        public void AddJobSeeker(ApplicationUser user)
        {
            user.Role = "JobSeeker";
            _userRepository.AddUser(user);
        }

        public ApplicationUser? GetJobSeeker(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public ApplicationUser? UpadeEmployeeById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public void Update(int id, ApplicationUser user)
        {
            ApplicationUser existingUser = _userRepository.GetUserById(id);
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                existingUser.CompanyName = user.CompanyName;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.isActive = user.isActive;

                _userRepository.UpdateUser(existingUser);
            }
        }

        public void Delete(int id)
        {
            _userRepository.DeleteUser(id);
        } 
        
        public ApplicationUser? DeleteById(int id)
        {
            return _userRepository.GetUserById(id);
        }

    }
}
using OnlineJobPortal.Models;

namespace OnlineJobPortal.IServices
{
    public interface IAuthService
    {
        ApplicationUser? AuthenticateUser(string email, string password);
        ApplicationUser? GetUserByEmail(string email);
        void RegisterUser(ApplicationUser user);
    }
}

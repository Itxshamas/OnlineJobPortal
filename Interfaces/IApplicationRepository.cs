using OnlineJobPortal.Models;

namespace OnlineJobPortal.Interfaces
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<Application>> GetAllAsync();
        Task<Application?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
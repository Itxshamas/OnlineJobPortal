using OnlineJobPortal.Models;

namespace OnlineJobPortal.Interfaces
{
    public interface IResumeRepository
    {
        Task<IEnumerable<Resume>> GetAllAsync();
        Task<Resume> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}

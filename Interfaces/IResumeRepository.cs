using OnlineJobPortal.Models;

namespace OnlineJobPortal.Interfaces
{
    public interface IResumeRepository
    {
        Task<IEnumerable<Resume>> GetAllAsync();
        Task<Resume?> GetByIdAsync(int id);
        Task AddAsync(Resume resume);
        Task UpdateAsync(Resume resume);
        Task<bool> DeleteAsync(int id);
                Task<Resume?> GetByUserIdAsync(int UserId); // add '?', nullable


    }
}
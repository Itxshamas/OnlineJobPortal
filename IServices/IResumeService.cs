using OnlineJobPortal.DTOs;
using OnlineJobPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineJobPortal.IServices
{
    public interface IResumeService
    {
        Task<IEnumerable<ResumeDto>> GetAllResumesAsync();
        Task<bool> DeleteResumeAsync(int id);
    }
}

using OnlineJobPortal.DTOs;
using OnlineJobPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineJobPortal.IServices
{
    public interface IResumeService
    {
        Task<IEnumerable<ResumeDto>> GetAllResumesAsync();
        Task<ResumeDto?> GetResumeByUserIdAsync(int UserId);  // nullable if user has no resume
        Task<Resume?> GetByUserIdAsync(int UserId);  // already nullable

        Task<bool> UploadResumeAsync(int UserId, string fileName); // changed from IFormFile
        Task<bool> UpdateResumeAsync(int UserId, string fileName); // changed from IFormFile
        Task<bool> DeleteResumeAsync(int id);
    }
}

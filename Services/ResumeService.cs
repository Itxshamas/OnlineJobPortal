using OnlineJobPortal.DTOs;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineJobPortal.Services
{
    public class ResumeService : IResumeService
    {
        private readonly IResumeRepository _resumeRepository;

        public ResumeService(IResumeRepository resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        public async Task<IEnumerable<ResumeDto>> GetAllResumesAsync()
        {
            IEnumerable<Resume> resumes = await _resumeRepository.GetAllAsync();
            return resumes.Select(r => new ResumeDto
            {
                UserId = r.UserId,
                FileName = r.FileName,
                FilePath = r.FilePath,
                UploadedDate = r.UploadedDate
            });
        }

        public async Task<ResumeDto?> GetResumeByUserIdAsync(int userId)
        {
            Resume resume = await _resumeRepository.GetByUserIdAsync(userId);
            if (resume == null) return null;

            return new ResumeDto
            {
                UserId = resume.UserId,
                FileName = resume.FileName,
                FilePath = resume.FilePath,
                UploadedDate = resume.UploadedDate
            };
        }

        public async Task<Resume?> GetByUserIdAsync(int userId)
        {
            return await _resumeRepository.GetByUserIdAsync(userId);
        }
        public async Task<bool> UploadResumeAsync(int userId, string fileName)
        {
            Resume resume = await _resumeRepository.GetByUserIdAsync(userId);
            if (resume != null) return false; // Already exists

            Resume newResume = new Resume
            {
                UserId = userId,
                FileName = fileName,
                UploadedDate = DateTime.UtcNow
            };

            await _resumeRepository.AddAsync(newResume);
            return true;
        }

        public async Task<bool> UpdateResumeAsync(int userId, string fileName)
        {
            Resume resume = await _resumeRepository.GetByUserIdAsync(userId);
            if (resume == null) return false; // No resume to update

            resume.FileName = fileName;
            resume.UploadedDate = DateTime.UtcNow;

            await _resumeRepository.UpdateAsync(resume);
            return true;
        }

        public async Task<bool> DeleteResumeAsync(int id)
        {
            return await _resumeRepository.DeleteAsync(id);
        }
    }
}
using OnlineJobPortal.DTOs;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;
using OnlineJobPortal.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineJobPortal.Services
{
    public class ResumeService : IResumeService
    {
        private readonly ApplicationDbContext _context;

        public ResumeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResumeDto>> GetAllResumesAsync()
        {
            return await _context.Resumes
                .Select(r => new ResumeDto
                {
                    UserId = r.UserId,
                    FileName = r.FileName,
                    FilePath = r.FilePath,
                    UploadedDate = r.UploadedDate
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteResumeAsync(int id)
        {
            Resume resume = await _context.Resumes.FindAsync(id);
            if (resume == null) return false;

            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
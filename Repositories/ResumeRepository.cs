using Microsoft.EntityFrameworkCore;
using OnlineJobPortal.Data;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly ApplicationDbContext _context;

        public ResumeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resume>> GetAllAsync()
        {
            return await _context.Resumes.ToListAsync();
        }

        public async Task<Resume?> GetByIdAsync(int id)
        {
            return await _context.Resumes.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Resume resume = await _context.Resumes.FindAsync(id);
            if (resume == null) return false;

            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

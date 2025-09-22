using OnlineJobPortal.Data;
using OnlineJobPortal.Models;
using OnlineJobPortal.Interfaces.IServices;
using System.Collections.Generic;
using System.Linq;

namespace OnlineJobPortal.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext _context;
        public JobService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<JobPost> GetAllJobs()
        {
            return _context.JobPosts.ToList();
        }

        public bool UpdateJobStatus(int jobId, string status)
        {
            var job = _context.JobPosts.FirstOrDefault(j => j.Id == jobId);
            if (job == null) return false;

            job.Status = status;
            _context.SaveChanges();
            return true;
        }
    }
}

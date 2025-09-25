using OnlineJobPortal.Data;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineJobPortal.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<JobPost> GetAll()
        {
            return _context.JobPosts.ToList();
        }

        public JobPost? GetById(int id)
        {
            return _context.JobPosts.FirstOrDefault(j => j.Id == id);
        }

        public void Add(JobPost jobPost)
        {
            _context.JobPosts.Add(jobPost);
            _context.SaveChanges();
        }

        public void Update(JobPost jobPost)
        {
            _context.JobPosts.Update(jobPost);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            JobPost job = _context.JobPosts.FirstOrDefault(j => j.Id == id);
            if (job != null)
            {
                _context.JobPosts.Remove(job);
                _context.SaveChanges();
            }
        }
    }
}

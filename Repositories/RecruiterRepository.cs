using OnlineJobPortal.Data;
using OnlineJobPortal.Models;
using OnlineJobPortal.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OnlineJobPortal.Repositories
{
    public class RecruiterRepository : IRecruiterRepository
    {
        private readonly ApplicationDbContext _context;

        public RecruiterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Recruiter> GetAll()
        {
            return _context.Recruiters.ToList();
        }

        public Recruiter? GetById(int id)
        {
            return _context.Recruiters.FirstOrDefault(r => r.Id == id);
        }

        public void Add(Recruiter recruiter)
        {
            _context.Recruiters.Add(recruiter);
            _context.SaveChanges();
        }

        public void Update(Recruiter recruiter)
        {
            _context.Recruiters.Update(recruiter);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Recruiter recruiter = _context.Recruiters.FirstOrDefault(r => r.Id == id);
            if (recruiter != null)
            {
                _context.Recruiters.Remove(recruiter);
                _context.SaveChanges();
            }
        }
    }
}

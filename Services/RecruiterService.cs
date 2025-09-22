using OnlineJobPortal.Models;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.Interfaces.IServices;
using System.Collections.Generic;

namespace OnlineJobPortal.Services
{
    public class RecruiterService : IRecruiterService
    {
        private readonly IRecruiterRepository _recruiterRepository;

        public RecruiterService(IRecruiterRepository recruiterRepository)
        {
            _recruiterRepository = recruiterRepository;
        }

        public IEnumerable<Recruiter> GetAll()
        {
            return _recruiterRepository.GetAll();
        }

        public Recruiter? GetById(int id)
        {
            return _recruiterRepository.GetById(id);
        }

        public void Add(Recruiter recruiter)
        {
            _recruiterRepository.Add(recruiter);
        }

        public void Update(Recruiter recruiter)
        {
            _recruiterRepository.Update(recruiter);
        }

        public void Delete(int id)
        {
            _recruiterRepository.Delete(id);
        }
    }
}

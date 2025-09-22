using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.Interfaces
{
    public interface IRecruiterRepository
    {
        IEnumerable<Recruiter> GetAll();
        Recruiter? GetById(int id);
        void Add(Recruiter recruiter);
        void Update(Recruiter recruiter);
        void Delete(int id);
    }
}

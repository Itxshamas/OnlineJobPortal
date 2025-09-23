using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.IServices
{
    public interface IRecruiterService
    {
        IEnumerable<Recruiter> GetAll();
        Recruiter? GetById(int id);
        void Add(Recruiter recruiter);
        void Update(Recruiter recruiter);
        void Delete(int id);
    }
}

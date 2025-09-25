using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.Interfaces
{
    public interface IJobRepository
    {
        IEnumerable<JobPost> GetAll();
        JobPost? GetById(int id);
        void Add(JobPost jobPost);
        void Update(JobPost jobPost);
        void Delete(int id);
    }
}

using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.IServices
{
    public interface IJobService
    {
        List<JobPost> GetAllJobs();
        bool UpdateJobStatus(int jobId, string status);
    }
}

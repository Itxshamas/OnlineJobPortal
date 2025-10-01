using OnlineJobPortal.DTOs;
using System.Collections.Generic;

namespace OnlineJobPortal.IServices
{
    public interface IJobService
    {
        IEnumerable<JobPostDto> GetAllJobs(); //Admin-specific
        IEnumerable<JobPostDto> GetAllJobsByRecruiter(int recruiterId); // recruiter-specific
        JobPostDto? GetJobById(int id);
        void CreateJob(JobPostDto jobPostDto);
        void UpdateJob(int id, JobPostDto jobPostDto);
        void DeleteJob(int id);
    }
}
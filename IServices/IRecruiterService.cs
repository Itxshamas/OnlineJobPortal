using OnlineJobPortal.DTOs;
using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.IServices
{
    public interface IRecruiterService
    {
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser? GetById(int id);
        void Add(ApplicationUser user);
        void Update(int id, ApplicationUser user);
        void Delete(int id);
        ApplicationUser? GetProfile(int recruiterId);
        void UpdateRecProfile(int recruiterId, ApplicationUser user);
        JobPostDto CreateJobPost(JobPostDto jobPostDto, int recruiterId);
        JobPostDto? GetJobPostById(int jobId);
        void UpdateJob(int jobId, JobPostDto jobPostDto);
        void DeleteJob(int jobId);
    }
}

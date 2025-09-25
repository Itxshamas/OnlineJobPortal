using OnlineJobPortal.DTOs;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.Models;
using OnlineJobPortal.IServices;
using System.Collections.Generic;
using System.Linq;

namespace OnlineJobPortal.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        // Generic Methods (existing)
        public IEnumerable<JobPostDto> GetAllJobs()
        {
            IEnumerable<JobPost> jobs = _jobRepository.GetAll();
            return jobs.Select(j => MapToDto(j));
        }

        public JobPostDto? GetJobById(int id)
        {
            JobPost job = _jobRepository.GetById(id);
            if (job == null) return null;
            return MapToDto(job);
        }

        public void CreateJob(JobPostDto jobPostDto)
        {
            JobPost job = MapToEntity(jobPostDto);
            _jobRepository.Add(job);
        }

        public void UpdateJob(int id, JobPostDto jobPostDto)
        {
            JobPost job = _jobRepository.GetById(id);
            if (job == null) return;

            job.Title = jobPostDto.Title;
            job.Description = jobPostDto.Description;
            job.CompanyName = jobPostDto.CompanyName;
            job.CategoryId = jobPostDto.CategoryId;
            job.RecruiterId = jobPostDto.RecruiterId;
            job.NumberOfOpenings = jobPostDto.NumberOfOpenings;
            job.Location = jobPostDto.Location;
            job.SalaryRange = jobPostDto.SalaryRange;
            job.Deadline = jobPostDto.Deadline;
            job.Status = jobPostDto.Status ?? job.Status;

            _jobRepository.Update(job);
        }

        public void DeleteJob(int id)
        {
            _jobRepository.Delete(id);
        }

        // Recruiter-Specific Methods
        // Get all jobs for a specific recruiter
        public IEnumerable<JobPostDto> GetAllJobsByRecruiter(int recruiterId)
        {
            IEnumerable<JobPost> jobs = _jobRepository.GetAll()
                .Where(j => j.RecruiterId == recruiterId);
            return jobs.Select(j => MapToDto(j));
        }

        // Get a job by ID only if it belongs to the recruiter
        public JobPostDto? GetJobByIdForRecruiter(int id, int recruiterId)
        {
            JobPost job = _jobRepository.GetById(id);
            if (job == null || job.RecruiterId != recruiterId) return null;
            return MapToDto(job);
        }

        // Update job only if it belongs to the recruiter
        public void UpdateJobForRecruiter(int id, int recruiterId, JobPostDto jobPostDto)
        {
            JobPost job = _jobRepository.GetById(id);
            if (job == null || job.RecruiterId != recruiterId) return;

            job.Title = jobPostDto.Title;
            job.Description = jobPostDto.Description;
            job.CompanyName = jobPostDto.CompanyName;
            job.CategoryId = jobPostDto.CategoryId;
            job.NumberOfOpenings = jobPostDto.NumberOfOpenings;
            job.Location = jobPostDto.Location;
            job.SalaryRange = jobPostDto.SalaryRange;
            job.Deadline = jobPostDto.Deadline;
            job.Status = jobPostDto.Status ?? job.Status;

            _jobRepository.Update(job);
        }

        // Delete job only if it belongs to the recruiter
        public void DeleteJobForRecruiter(int id, int recruiterId)
        {
            JobPost job = _jobRepository.GetById(id);
            if (job == null || job.RecruiterId != recruiterId) return;

            _jobRepository.Delete(id);
        }

        // Helper Methods
        private JobPostDto MapToDto(JobPost job)
        {
            return new JobPostDto
            {
                Title = job.Title,
                Description = job.Description,
                CompanyName = job.CompanyName,
                CategoryId = job.CategoryId,
                RecruiterId = job.RecruiterId,
                NumberOfOpenings = job.NumberOfOpenings,
                Location = job.Location,
                SalaryRange = job.SalaryRange,
                Deadline = job.Deadline,
                Status = job.Status,
                PostedDate = job.PostedDate
            };
        }

        private JobPost MapToEntity(JobPostDto dto)
        {
            return new JobPost
            {
                Title = dto.Title,
                Description = dto.Description,
                CompanyName = dto.CompanyName,
                CategoryId = dto.CategoryId,
                RecruiterId = dto.RecruiterId,
                NumberOfOpenings = dto.NumberOfOpenings,
                Location = dto.Location,
                SalaryRange = dto.SalaryRange,
                Deadline = dto.Deadline,
                Status = dto.Status ?? "Pending",
                PostedDate = dto.PostedDate != default ? dto.PostedDate : System.DateTime.Now
            };
        }
    }
}

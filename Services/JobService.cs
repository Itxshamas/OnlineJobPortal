using OnlineJobPortal.DTOs;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.Models;
using OnlineJobPortal.IServices;

namespace OnlineJobPortal.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public IEnumerable<JobPostDto> GetAllJobs()
        {
            IEnumerable<JobPost> jobs = _jobRepository.GetAll();
            return jobs.Select(j => new JobPostDto
            {
                Title = j.Title,
                Description = j.Description,
                CompanyName = j.CompanyName,
                CategoryId = j.CategoryId,
                RecruiterId = j.RecruiterId,
                NumberOfOpenings = j.NumberOfOpenings,
                Location = j.Location,
                SalaryRange = j.SalaryRange,
                Deadline = j.Deadline,
                Status = j.Status,
                PostedDate = j.PostedDate
            });
        }

        public JobPostDto? GetJobById(int id)
        {
            var job = _jobRepository.GetById(id);
            if (job == null) return null;

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

        public void CreateJob(JobPostDto jobPostDto)
        {
            var job = new JobPost
            {
                Title = jobPostDto.Title,
                Description = jobPostDto.Description,
                CompanyName = jobPostDto.CompanyName,
                CategoryId = jobPostDto.CategoryId,
                RecruiterId = jobPostDto.RecruiterId,
                NumberOfOpenings = jobPostDto.NumberOfOpenings,
                Location = jobPostDto.Location,
                SalaryRange = jobPostDto.SalaryRange,
                Deadline = jobPostDto.Deadline,
                Status = jobPostDto.Status ?? "Pending",
                PostedDate = jobPostDto.PostedDate != default ? jobPostDto.PostedDate : System.DateTime.Now
            };

            _jobRepository.Add(job);
        }

        public void UpdateJob(int id, JobPostDto jobPostDto)
        {
            var job = _jobRepository.GetById(id);
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
    }
}

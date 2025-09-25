using OnlineJobPortal.Models;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.IServices;
using System.Collections.Generic;
using OnlineJobPortal.DTOs;
using System.Linq;

namespace OnlineJobPortal.Services
{
    public class RecruiterService : IRecruiterService
    {
        private readonly IRecruiterRepository _recruiterRepository;
        private readonly IJobRepository _jobRepository;
        public RecruiterService(IRecruiterRepository recruiterRepository, IJobRepository jobRepository)
        {
            _recruiterRepository = recruiterRepository;
            _jobRepository = jobRepository;
        }

        public IEnumerable<RecruiterDto> GetAll()
        {
            IEnumerable<Recruiter> recruiters = _recruiterRepository.GetAll();
            return recruiters.Select(recruiter => new RecruiterDto
            {
                Id = recruiter.Id,
                FullName = recruiter.FullName,
                CompanyName = recruiter.CompanyName,
                Email = recruiter.Email,
                Phone = recruiter.Phone,
                IsActive = recruiter.IsActive
            });
        }

        public RecruiterDto? GetById(int id)
        {
            Recruiter recruiter = _recruiterRepository.GetById(id);
            if (recruiter == null)
            {
                return null;
            }

            return new RecruiterDto
            {
                Id = recruiter.Id,
                FullName = recruiter.FullName,
                CompanyName = recruiter.CompanyName,
                Email = recruiter.Email,
                Phone = recruiter.Phone,
                IsActive = recruiter.IsActive
            };
        }

        public void Add(RecruiterDto recruiterDto)
        {
            Recruiter recruiter = new Recruiter
            {
                FullName = recruiterDto.FullName,
                CompanyName = recruiterDto.CompanyName,
                Email = recruiterDto.Email,
                Phone = recruiterDto.Phone,
                IsActive = recruiterDto.IsActive
            };
            _recruiterRepository.Add(recruiter);
        }

        public void Update(int id, RecruiterDto recruiterDto)
        {
            Recruiter recruiter = _recruiterRepository.GetById(id);
            if (recruiter != null)
            {
                recruiter.FullName = recruiterDto.FullName;
                recruiter.CompanyName = recruiterDto.CompanyName;
                recruiter.Email = recruiterDto.Email;
                recruiter.Phone = recruiterDto.Phone;
                recruiter.IsActive = recruiterDto.IsActive;
                _recruiterRepository.Update(recruiter);
            }
        }

        public void Delete(int id)
        {
            _recruiterRepository.Delete(id);
        }

        public JobPostDto CreateJobPost(JobPostDto jobPostDto, int recruiterId)
{
    jobPostDto.RecruiterId = recruiterId;
    jobPostDto.PostedDate = DateTime.Now;
    jobPostDto.Status ??= "Pending";

    JobPost jobPost = new JobPost
    {
        Title = jobPostDto.Title,
        Description = jobPostDto.Description,
        CompanyName = jobPostDto.CompanyName,
        CategoryId = jobPostDto.CategoryId,
        RecruiterId = jobPostDto.RecruiterId,
        Status = jobPostDto.Status,
        PostedDate = jobPostDto.PostedDate,
        NumberOfOpenings = jobPostDto.NumberOfOpenings,
        Location = jobPostDto.Location,
        SalaryRange = jobPostDto.SalaryRange,
        Deadline = jobPostDto.Deadline,
        IsActive = true
    };

    _jobRepository.Add(jobPost);

    // Return DTO to controller
    return jobPostDto;
}

    }
}

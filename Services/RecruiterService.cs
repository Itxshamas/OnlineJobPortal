using OnlineJobPortal.Models;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.IServices;
using OnlineJobPortal.DTOs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OnlineJobPortal.Services
{
    public class RecruiterService : IRecruiterService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;

        public RecruiterService(IUserRepository userRepository, IJobRepository jobRepository)
        {
            _userRepository = userRepository;
            _jobRepository = jobRepository;
        }

        //  Get all recruiters
        public IEnumerable<ApplicationUserDto> GetAll()
        {
            return _userRepository.GetUsersByRole("Recruiter").Select(u=> new ApplicationUserDto
            {
              Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                isActive = u.isActive,
                Phone = u.Phone,
                CompanyName = u.CompanyName,
                CreatedAt = u.CreatedAt
            });
        }

        //  Get recruiter by Id
        public ApplicationUser? GetById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        // Get profile for logged-in recruiter
        public ApplicationUser? GetProfile(int recruiterId)
        {
            return _userRepository.GetUserById(recruiterId);
        }

        // Update profile for logged-in recruiter
        public void UpdateRecProfile(int recruiterId, ApplicationUser user)
        {
            ApplicationUser existingUser = _userRepository.GetUserById(recruiterId);
            if (existingUser == null) return;

            existingUser.FullName = user.FullName;
            existingUser.CompanyName = user.CompanyName;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.isActive = user.isActive;

            _userRepository.UpdateUser(existingUser);
        }

        // Add new recruiter
        public void Add(ApplicationUser user)
        {
            user.Role = "Recruiter";
            _userRepository.AddUser(user);
        }

        // Update recruiter by Id
        public void Update(int id, ApplicationUser user)
        {
            ApplicationUser existingUser = _userRepository.GetUserById(id);
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                existingUser.CompanyName = user.CompanyName;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.isActive = user.isActive;

                _userRepository.UpdateUser(existingUser);
            }
        }

        // Delete recruiter
        public void Delete(int id)
        {
            _userRepository.DeleteUser(id);
        }

        // Create a new job post (for logged-in recruiter)
        public JobPostDto CreateJobPost(JobPostDto jobPostDto, int recruiterId)
        {
            jobPostDto.RecruiterId = recruiterId;
            jobPostDto.PostedDate = DateTime.Now;
            jobPostDto.Status = "Pending";

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

            return jobPostDto;
        }

        //  Get job post by Id
        public JobPostDto? GetJobPostById(int jobId)
        {
            JobPost? job = _jobRepository.GetById(jobId);
            if (job == null) return null;

            return new JobPostDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                CompanyName = job.CompanyName,
                CategoryId = job.CategoryId,
                RecruiterId = job.RecruiterId,
                Status = job.Status,
                PostedDate = job.PostedDate,
                NumberOfOpenings = job.NumberOfOpenings,
                Location = job.Location,
                SalaryRange = job.SalaryRange,
                Deadline = job.Deadline
            };
        }

        //  Update job post
        public void UpdateJob(int jobId, JobPostDto jobPostDto)
        {
            JobPost? job = _jobRepository.GetById(jobId);
            if (job == null) return;

            job.Title = jobPostDto.Title;
            job.Description = jobPostDto.Description;
            job.CompanyName = jobPostDto.CompanyName;
            job.CategoryId = jobPostDto.CategoryId;
            job.Status = jobPostDto.Status;
            job.NumberOfOpenings = jobPostDto.NumberOfOpenings;
            job.Location = jobPostDto.Location;
            job.SalaryRange = jobPostDto.SalaryRange;
            job.Deadline = jobPostDto.Deadline;

            _jobRepository.Update(job);
        }

        //  Delete job post
        public void DeleteJob(int jobId)
        {
            _jobRepository.Delete(jobId);
        }
    }
}

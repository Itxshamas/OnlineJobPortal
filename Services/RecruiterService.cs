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

        public RecruiterService(IRecruiterRepository recruiterRepository)
        {
            _recruiterRepository = recruiterRepository;
        }

        public IEnumerable<RecruiterDto> GetAll()
        {
            var recruiters = _recruiterRepository.GetAll();
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
            var recruiter = _recruiterRepository.GetById(id);
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
            var recruiter = new Recruiter
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
            var recruiter = _recruiterRepository.GetById(id);
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
    }
}

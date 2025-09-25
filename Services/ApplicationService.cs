using OnlineJobPortal.DTOs;
using OnlineJobPortal.Interfaces;
using OnlineJobPortal.IServices;
using OnlineJobPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineJobPortal.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _appRepo;

        public ApplicationService(IApplicationRepository appRepo)
        {
            _appRepo = appRepo;
        }

        public async Task<IEnumerable<ApplicationDto>> GetAllApplicationsAsync()
        {
            var apps = await _appRepo.GetAllAsync();

            // Map Application -> ApplicationDto
            return apps.Select(a => new ApplicationDto
            {
                JobPostId = a.JobPostId,
                UserId = a.UserId,
                AppliedDate = a.AppliedDate
            });
        }

        public async Task<bool> DeleteApplicationAsync(int id)
        {
            return await _appRepo.DeleteAsync(id);
        }
    }
}

using OnlineJobPortal.DTOs;

namespace OnlineJobPortal.IServices
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDto>> GetAllApplicationsAsync();
        Task<bool> DeleteApplicationAsync(int id);
    }
}

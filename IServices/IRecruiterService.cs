using OnlineJobPortal.DTOs;
using OnlineJobPortal.Models;
using System.Collections.Generic;

namespace OnlineJobPortal.IServices
{
    public interface IRecruiterService
    {
        IEnumerable<RecruiterDto> GetAll();
        RecruiterDto? GetById(int id);
        void Add(RecruiterDto recruiterDto);
        void Update(int id, RecruiterDto recruiterDto);
        void Delete(int id);
    }
}

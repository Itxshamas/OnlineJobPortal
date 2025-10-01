using System;

namespace OnlineJobPortal.DTOs
{
    public class ApplicationDto
    {
        public int JobPostId { get; set; }
        public int UserId { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
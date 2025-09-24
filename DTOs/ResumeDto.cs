using System;

namespace OnlineJobPortal.DTOs
{
    public class ResumeDto
    {
        public int UserId { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public DateTime? UploadedDate { get; set; }
    }
}

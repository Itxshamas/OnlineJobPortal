
namespace OnlineJobPortal.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string? FilePath { get; set; }
        public DateTime UploadedDate { get; set; } = DateTime.Now;

    }
}
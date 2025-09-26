namespace OnlineJobPortal.Models
{
     public class Recruiter
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
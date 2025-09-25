namespace OnlineJobPortal.Models
{
    public class JobPost
    {
        public int Id { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CompanyName { get; set; }
        public int CategoryId { get; set; }
        public int RecruiterId { get; set; }
        public bool IsActive { get; set; } = true;
        public string Status { get; set; } = "Pending";
        public int NumberOfOpenings { get; set; }
        public string? Location { get; set; }
        public string? SalaryRange { get; set; }
        public DateTime Deadline { get; set; }
    }
 }

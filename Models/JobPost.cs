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
        public string Status { get; set; } = "Pending";
    }
 }

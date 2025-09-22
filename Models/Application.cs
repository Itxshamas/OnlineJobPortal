namespace OnlineJobPortal.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int JobPostId { get; set; }
        public int UserId { get; set; }
        public DateTime AppliedDate { get; set; } = DateTime.Now;
    }
}

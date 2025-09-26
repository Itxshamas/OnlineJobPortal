namespace OnlineJobPortal.Models
{
    public class AdminLog
    {
        public int Id { get; set; }
        public string? Action { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.Now;
        public int AdminId { get; set; }
    }
}
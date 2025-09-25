namespace OnlineJobPortal.DTOs
{
    public class JobPostDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CompanyName { get; set; }
        public int CategoryId { get; set; }
        public int RecruiterId { get; set; }
        public string? Status { get; set; }
        public DateTime PostedDate { get; set; }
        public int NumberOfOpenings { get; set; }
        public string? Location { get; set; }
        public string? SalaryRange { get; set; }
        public DateTime Deadline { get; set; }
    }
}

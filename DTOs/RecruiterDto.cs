namespace OnlineJobPortal.DTOs
{
    public class RecruiterDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }       
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
    }
}

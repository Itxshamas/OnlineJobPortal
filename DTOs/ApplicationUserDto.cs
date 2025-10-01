namespace OnlineJobPortal.DTOs
{
    public class ApplicationUserDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool isActive { get; set; }
        public string? Phone { get; set; }
        public string? CompanyName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

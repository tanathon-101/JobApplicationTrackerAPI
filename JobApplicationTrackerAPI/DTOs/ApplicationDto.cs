namespace JobApplicationTrackerAPI.DTOs
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime? InterviewDate { get; set; }
    }
}
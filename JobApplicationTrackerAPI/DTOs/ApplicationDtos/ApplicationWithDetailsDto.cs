using JobApplicationTrackerAPI.Model;

namespace JobApplicationTrackerAPI.DTOs.ApplicationDtos
{
    public class ApplicationWithDetailsDto
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string PositionTitle { get; set; }

        public ApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string? Notes { get; set; }
    }
}
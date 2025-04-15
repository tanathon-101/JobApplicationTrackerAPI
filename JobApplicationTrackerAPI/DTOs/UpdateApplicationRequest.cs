using JobApplicationTrackerAPI.Model;

namespace JobApplicationTrackerAPI.DTOs
{
    public class UpdateApplicationRequest
    {
        public int CompanyId { get; set; }
        public int PositionId { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string? Notes { get; set; }
    }
}
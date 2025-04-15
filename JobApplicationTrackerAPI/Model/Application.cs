namespace JobApplicationTrackerAPI.Model
{
    public class Application
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int PositionId { get; set; }

        public ApplicationStatus Status { get; set; }
        public DateTime AppliedDate { get; set; }
        public DateTime? InterviewDate { get; set; }
        public string? Notes { get; set; }
    }
}

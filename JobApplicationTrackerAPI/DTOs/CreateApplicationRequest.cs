namespace JobApplicationTrackerAPI.DTOs
{
    public class CreateApplicationRequest
    {
        public int CompanyId { get; set; }
        public int PositionId { get; set; }
        public DateTime AppliedDate { get; set; }
        public string? Notes { get; set; }
    }
}
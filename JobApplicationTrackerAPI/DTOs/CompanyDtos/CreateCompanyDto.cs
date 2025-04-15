namespace JobApplicationTrackerAPI.DTOs.CompanyDtos
{
    public class CreateCompanyDto
    {
        public string Name { get; set; }
        public string Website { get; set; } = "";
    }
}
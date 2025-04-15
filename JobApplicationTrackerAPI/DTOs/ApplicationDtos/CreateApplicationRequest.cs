namespace JobApplicationTrackerAPI.DTOs
{
    public class CreateApplicationRequest
    {
        public int UserId { get; set; }        // ผู้สมัคร
        public int CompanyId { get; set; }     // บริษัทที่สมัคร
        public int PositionId { get; set; }    // ตำแหน่งที่สมัคร
        public DateTime AppliedDate { get; set; } // วันที่สมัคร
        public DateTime? InterviewDate { get; set; } // (optional) นัดสัมภาษณ์
        public string? Notes { get; set; }     // หมายเหตุเพิ่มเติม
    }
}
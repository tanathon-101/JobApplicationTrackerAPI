using JobApplicationTrackerAPI.Model;

namespace JobApplicationTrackerAPI.DTOs
{
    public class UpdateApplicationRequest
    {
       public int CompanyId { get; set; }         // อาจอัปเดตบริษัท
        public int PositionId { get; set; }        // หรือเปลี่ยนตำแหน่งที่สมัคร
        public ApplicationStatus Status { get; set; }  // เปลี่ยนสถานะ เช่น Interviewed, Hired
        public DateTime AppliedDate { get; set; }      // แก้วันสมัคร
        public DateTime? InterviewDate { get; set; }   // วันสัมภาษณ์
        public string? Notes { get; set; }      
    }
}
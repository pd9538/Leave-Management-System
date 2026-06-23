using System.ComponentModel.DataAnnotations;

namespace leave_management_api.DTOs
{
    public class ApplyLeaveDto
    {
        [Required]
        public Guid LeaveTypeId { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;
    }
}


using leave_management_api.Enums;

namespace leave_management_api.Models
{
    public class LeaveRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public Guid LeaveTypeId { get; set; }

        public LeaveType? LeaveType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; }

        public DateTime AppliedDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}

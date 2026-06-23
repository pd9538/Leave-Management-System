namespace leave_management_api.DTOs
{
    public class LeaveResponseDto
    {
        public Guid Id { get; set; }

        public string LeaveType { get; set; } = string.Empty;

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime AppliedDate { get; set; }
    }
}

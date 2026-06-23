namespace leave_management_api.DTOs
{
    public class DashboardDto
    {
        public int TotalEmployees { get; set; }

        public int TotalLeaves { get; set; }

        public int PendingLeaves { get; set; }

        public int ApprovedLeaves { get; set; }

        public int RejectedLeaves { get; set; }
    }
}

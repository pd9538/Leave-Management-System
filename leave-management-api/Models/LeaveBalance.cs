namespace leave_management_api.Models
{
    public class LeaveBalance
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public int SickLeave { get; set; }

        public int CasualLeave { get; set; }

        public int EarnedLeave { get; set; }
    }
}
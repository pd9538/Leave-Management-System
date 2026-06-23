using leave_management_api.DTOs;

namespace leave_management_api.Interfaces
{
    public interface ILeaveService
    {
        Task<string> ApplyLeaveAsync(Guid userId,ApplyLeaveDto dto);
        Task<List<LeaveResponseDto>> GetMyLeavesAsync(Guid userId);
        Task<List<LeaveResponseDto>> GetAllLeavesAsync();
        Task<string> ApproveLeaveAsync(Guid leaveId);
        Task<string> RejectLeaveAsync(Guid leaveId);
        Task<LeaveBalanceDto> GetLeaveBalanceAsync(Guid userId);
    }
}

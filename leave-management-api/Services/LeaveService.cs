using leave_management_api.Data;
using leave_management_api.DTOs;
using leave_management_api.Enums;
using leave_management_api.Interfaces;
using leave_management_api.Models;
using Microsoft.EntityFrameworkCore;

namespace leave_management_api.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly LeaveDbContext _context;

        public LeaveService(LeaveDbContext context)
        {
            _context = context;
        }

        public async Task<string> ApplyLeaveAsync(
            Guid userId,
            ApplyLeaveDto dto)
        {
            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(x => x.Id == dto.LeaveTypeId);

            if (leaveType == null)
                throw new Exception("Invalid Leave Type");

            var leave = new LeaveRequest
            {
                Id = Guid.NewGuid(),

                UserId = userId,

                LeaveTypeId = dto.LeaveTypeId,

                FromDate = dto.FromDate,

                ToDate = dto.ToDate,

                Reason = dto.Reason,

                Status = LeaveStatus.Pending,

                AppliedDate = DateTime.UtcNow
            };

            _context.LeaveRequests.Add(leave);

            await _context.SaveChangesAsync();

            return "Leave Applied Successfully";
        }

        public async Task<List<LeaveResponseDto>> GetMyLeavesAsync(Guid userId)
        {
            return await _context.LeaveRequests

                .Include(x => x.LeaveType)

                .Where(x => x.UserId == userId)

                .OrderByDescending(x => x.AppliedDate)

                .Select(x => new LeaveResponseDto
                {
                    Id = x.Id,

                    LeaveType = x.LeaveType!.name,

                    FromDate = x.FromDate,

                    ToDate = x.ToDate,

                    Reason = x.Reason,

                    Status = x.Status.ToString(),

                    AppliedDate = x.AppliedDate
                })

                .ToListAsync();
        }

        public async Task<List<LeaveResponseDto>> GetAllLeavesAsync()
        {
            return await _context.LeaveRequests

                .Include(x => x.User)

                .Include(x => x.LeaveType)

                .OrderByDescending(x => x.AppliedDate)

                .Select(x => new LeaveResponseDto
                {
                    Id = x.Id,

                    LeaveType = x.LeaveType!.name,

                    FromDate = x.FromDate,

                    ToDate = x.ToDate,

                    Reason = x.Reason,

                    Status = x.Status.ToString(),

                    AppliedDate = x.AppliedDate
                })

                .ToListAsync();
        }

        public async Task<string> ApproveLeaveAsync(Guid leaveId)
        {
            var leave = await _context.LeaveRequests
                .FirstOrDefaultAsync(x => x.Id == leaveId);

            if (leave == null)
                throw new Exception("Leave not found");

            if (leave.Status == LeaveStatus.Approved)
                throw new Exception("Leave is already approved");

            if (leave.Status == LeaveStatus.Rejected)
                throw new Exception("Rejected leave cannot be approved");

            leave.Status = LeaveStatus.Approved;
            var leaveBalance = await _context.LeaveBalances.FirstOrDefaultAsync(x => x.UserId == leave.UserId);

            if (leaveBalance == null)
                throw new Exception("Leave Balance not found");
          
            int totalDays =(leave.ToDate.Date - leave.FromDate.Date).Days + 1;
            var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == leave.LeaveTypeId);

            if (leaveType == null)
                throw new Exception("Leave Type not found");
            if (leaveType.name == "Sick Leave")
            {
                if (leaveBalance.SickLeave < totalDays)
                    throw new Exception("Insufficient Sick Leave");

                leaveBalance.SickLeave -= totalDays;
            }

            else if (leaveType.name == "Casual Leave")
            {
                if (leaveBalance.CasualLeave < totalDays)
                    throw new Exception("Insufficient Casual Leave");

                leaveBalance.CasualLeave -= totalDays;
            }

            else if (leaveType.name == "Earned Leave")
            {
                if (leaveBalance.EarnedLeave < totalDays)
                    throw new Exception("Insufficient Earned Leave");

                leaveBalance.EarnedLeave -= totalDays;
            }

            leave.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return "Leave Approved Successfully";
        }

        public async Task<string> RejectLeaveAsync(Guid leaveId)
        {
            var leave = await _context.LeaveRequests
                .FirstOrDefaultAsync(x => x.Id == leaveId);

            if (leave == null)
                throw new Exception("Leave not found");

            if (leave.Status == LeaveStatus.Rejected)
                throw new Exception("Leave is already rejected");

            if (leave.Status == LeaveStatus.Approved)
                throw new Exception("Approved leave cannot be rejected");

            leave.Status = LeaveStatus.Rejected;

            leave.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return "Leave Rejected Successfully";
        }

        public async Task<LeaveBalanceDto> GetLeaveBalanceAsync(Guid userId)
        {
            var balance = await _context.LeaveBalances
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (balance == null)
                throw new Exception("Leave balance not found.");

            return new LeaveBalanceDto
            {
                SickLeave = balance.SickLeave,

                CasualLeave = balance.CasualLeave,

                EarnedLeave = balance.EarnedLeave
            };
        }
    }
}
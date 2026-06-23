using leave_management_api.Data;
using leave_management_api.DTOs;
using leave_management_api.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace leave_management_api.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class Dashboard:ControllerBase
    {
        private readonly LeaveDbContext _context;
        public Dashboard(LeaveDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<DashboardDto> GetDashboardAsync()
        {
            return new DashboardDto
            {
                TotalEmployees = await _context.Users.CountAsync(),

                TotalLeaves = await _context.LeaveRequests.CountAsync(),

                PendingLeaves = await _context.LeaveRequests
                    .CountAsync(x => x.Status == LeaveStatus.Pending),

                ApprovedLeaves = await _context.LeaveRequests
                    .CountAsync(x => x.Status == LeaveStatus.Approved),

                RejectedLeaves = await _context.LeaveRequests
                    .CountAsync(x => x.Status == LeaveStatus.Rejected)
            };
        }
    }
}

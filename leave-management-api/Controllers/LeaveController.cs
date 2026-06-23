using System.Security.Claims;
using leave_management_api.DTOs;
using leave_management_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace leave_management_api.Controllers
{
    [Authorize]

    [ApiController]

    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(
            ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyLeave(
            ApplyLeaveDto dto)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);

            var result = await _leaveService
                .ApplyLeaveAsync(userId, dto);

            return Ok(result);
        }

        [HttpGet("my-leaves")]
        public async Task<IActionResult> GetMyLeaves()
        {
            var userId = Guid.Parse(

                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);

            var result = await _leaveService
                .GetMyLeavesAsync(userId);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet("all")]
        public async Task<IActionResult> GetAllLeaves()
        {
            var result = await _leaveService.GetAllLeavesAsync();

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]

        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveLeave(Guid id)
        {
            var result =
                await _leaveService.ApproveLeaveAsync(id);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]

        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectLeave(Guid id)
        {
            var result =
                await _leaveService.RejectLeaveAsync(id);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier)!);

            var result = await _leaveService
                .GetLeaveBalanceAsync(userId);

            return Ok(result);
        }
    }
}
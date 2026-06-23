using leave_management_api.DTOs;

namespace leave_management_api.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}

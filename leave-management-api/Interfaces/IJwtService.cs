namespace leave_management_api.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}

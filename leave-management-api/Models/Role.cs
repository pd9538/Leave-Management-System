namespace leave_management_api.Models
{
    public class Role
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string RoleName { get; set; } = string.Empty;
    }
}

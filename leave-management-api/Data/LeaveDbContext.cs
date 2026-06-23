using leave_management_api.Models;
using Microsoft.EntityFrameworkCore;

namespace leave_management_api.Data
{
    public class LeaveDbContext:DbContext
    {
        public LeaveDbContext(DbContextOptions<LeaveDbContext> options)
       : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public DbSet<LeaveBalance> LeaveBalances { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(

                new Role
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    RoleName = "Admin"
                },

                new Role
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    RoleName = "Employee"
                }
            );

            modelBuilder.Entity<LeaveType>().HasData(

                new LeaveType
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    name = "Sick Leave"
                },

                new LeaveType
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    name = "Casual Leave"
                },

                new LeaveType
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    name = "Earned Leave"
                }
            );
        }
    }
}

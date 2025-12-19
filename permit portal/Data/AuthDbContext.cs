using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace permit_portal.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Directly seeding some roles to the dbcontext class
            var adminRoleId = "44b1cc08-da84-4c55-a016-e884a366ea43";
            var userRoleId = "15d1e987-e802-4bcc-bded-e2e4f77b7a09";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }

            };
            // this line tells the EF core to seed the identity role to the database
            builder.Entity<IdentityRole>().HasData(roles);

            var adminId = "b3b701f0-7bbf-489e-9578-bb735ac95d81";
            var adminUser = new IdentityUser()
            {
                UserName= "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                Id = adminId,
            };
            // this line hash the password securely before adding into the database
            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "Superadmin@123");

            // this seeds the data of the Admin User to the database
            builder.Entity<IdentityUser>().HasData(adminUser);

            //Giving the control of the user to the admin role
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    RoleId = adminRoleId,
                    UserId = adminId,
                },

                 new IdentityUserRole<string>()
                {
                    RoleId = userRoleId,
                    UserId = adminId,
                }

            };
            
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}

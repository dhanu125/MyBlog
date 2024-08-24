using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MYBlog.Data
{
    public class AuthDbContext:IdentityDbContext
    {
      public AuthDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var adminId = "baced6c2-4a7c-4b21-b510-89bd63f1cce1";
            var superadminId = "0f5f2d47-738c-4be6-8d6c-3d15351aaa53";
            var userId = "f80a936a-4ee0-4d12-a322-60a2b80c8098";
            var roles = new List<IdentityRole> {
                new IdentityRole
                {
                    Name= "admin",
                    NormalizedName= "admin",
                    Id=adminId,
                    ConcurrencyStamp=adminId
                },
                new IdentityRole
                {
                    Name= "superAdmin",
                    NormalizedName= "superAdmin",
                    Id=superadminId,
                    ConcurrencyStamp=superadminId
                },
                new IdentityRole
                {
                    Name= "user",
                    NormalizedName= "user",
                    Id=userId,
                    ConcurrencyStamp=userId
                }

            };
            builder.Entity<IdentityRole>().HasData(roles);
            var superadminuserId = "0959ba28-4303-4663-a42c-e81f2abda7d5";
            var superadmin = new IdentityUser
            {
                UserName = "patildhnu4111999@gmail.com",
                NormalizedUserName = "patildhnu4111999@gmail.com",
                Email = "patildhnu4111999@gmail.com",
                NormalizedEmail = "patildhnu4111999@gmail.com",
                Id=superadminuserId ,
                
            };
            superadmin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superadmin, "dhanashri@123");
            builder.Entity<IdentityUser>().HasData(superadmin);
            var superadminUserRole = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId=superadminuserId,
                    RoleId=adminId
                },
                new IdentityUserRole<string>
                {
                    UserId=superadminuserId,
                    RoleId=superadminId
                },
                new IdentityUserRole<string>
                {
                    UserId=superadminuserId,
                    RoleId=userId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superadminUserRole);
        }
    }
}

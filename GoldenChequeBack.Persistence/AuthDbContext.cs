using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoldenChequeBack.Persistence
{
    public class AuthDbContext :  IdentityDbContext
    {
        public AuthDbContext (DbContextOptions<AuthDbContext> options) : base (options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readRoleId = "37675fe6-e805-4229-a16a-e06757e0b616";
            var WriteRoleId = "b03d265b-2e5e-469b-be3c-99152ded05be";
            //create reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readRoleId,
                    Name ="Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readRoleId
                } ,
                new IdentityRole()
                {
                    Id = WriteRoleId,
                    Name ="Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = WriteRoleId
                }
            };
            //see the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create an admin User
            var adminUserId = "525eca66-9a9b-4f5d-bfc1-003fb2c2d95e";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@GoldCHQ.com",
                Email = "admin@GoldCHQ.com",
                NormalizedEmail = "admin@GoldCHQ.com".ToUpper(),
                NormalizedUserName = "admin@GoldCHQ.com".ToUpper()
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@12345");
            builder.Entity<IdentityUser>().HasData(admin);

            //Give Roles to Admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = WriteRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);



        }





    }

}

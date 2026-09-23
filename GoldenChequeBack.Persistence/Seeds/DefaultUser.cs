using GoldenChequeBack.Domain.Auth;
using GoldenChequeBack.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GoldenChequeBack.Persistence.Seeds
{
    public static class DefaultUser
    {
        public static List<ApplicationUser> IdentityBasicUserList()
        {
            var superAdmin = new ApplicationUser
            {
                Id = Constants.SuperAdminUser,
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "admin",
                LastName = "user",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                NormalizedUserName = "SUPERADMIN",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            superAdmin.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(superAdmin, "Admin@12345");

            var basicUser = new ApplicationUser
            {
                Id = Constants.BasicUser,
                UserName = "basicuser",
                Email = "basicuser@gmail.com",
                FirstName = "Basic",
                LastName = "User",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = "BASICUSER@GMAIL.COM",
                NormalizedUserName = "BASICUSER",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            basicUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(basicUser, "basicuser@12345");

            return new List<ApplicationUser> { superAdmin, basicUser };
        }
    }
}

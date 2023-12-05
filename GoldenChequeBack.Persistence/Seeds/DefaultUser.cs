using GoldenChequeBack.Domain.Auth;
using GoldenChequeBack.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GoldenChequeBack.Persistence.Seeds
{
    public static class DefaultUser
    {
        public static List<ApplicationUser> IdentityBasicUserList()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Id = Constants.SuperAdminUser,
                    UserName = "superadmin",
                    Email = "superadmin@gmail.com",
                    FirstName = "admin",
                    LastName = "user",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser(),"Admin@12345"),
                    NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                    NormalizedUserName="SUPERADMIN"
                },
                new ApplicationUser
                {
                    Id = Constants.BasicUser,
                    UserName = "basicuser",
                    Email = "basicuser@gmail.com",
                    FirstName = "Basic",
                    LastName = "User",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser(),"basicuser@12345"),
                    NormalizedEmail= "BASICUSER@GMAIL.COM",
                    NormalizedUserName="BASICUSER"
                },
            };
        }
    }
}

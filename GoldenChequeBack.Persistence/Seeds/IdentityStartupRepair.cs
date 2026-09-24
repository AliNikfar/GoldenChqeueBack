using GoldenChequeBack.Domain.Auth;
using GoldenChequeBack.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Persistence.Seeds
{
    /// <summary>
    /// Runs at application startup (after migrations) and makes sure the seeded
    /// Identity users always have a VALID Identity V3 password hash.
    /// Old local databases created from earlier versions of this solution may
    /// contain users whose PasswordHash is not valid base-64; signing in with
    /// such a user throws System.FormatException from PasswordHasher.
    /// This repair resets the seeded users' hashes to the documented passwords
    /// and reports any other corrupted accounts in the log.
    /// </summary>
    public static class IdentityStartupRepair
    {
        private const string SuperAdminEmail = "superadmin@gmail.com";
        private const string SuperAdminPassword = "Admin@12345";
        private const string BasicUserEmail = "basicuser@gmail.com";
        private const string BasicUserPassword = "basicuser@12345";

        public static async Task RepairSeededUsersAsync(IdentityContext db, ILogger logger)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            await EnsureSeedUserAsync(db, logger, hasher, SuperAdminEmail, "superadmin", SuperAdminPassword, Constants.SuperAdminUser);
            await EnsureSeedUserAsync(db, logger, hasher, BasicUserEmail, "basicuser", BasicUserPassword, Constants.BasicUser);

            var corruptOthers = await db.Users
                .AsNoTracking()
                .Where(u => u.Email != SuperAdminEmail && u.Email != BasicUserEmail)
                .ToListAsync();

            foreach (var user in corruptOthers.Where(u => !IsValidV3Hash(u.PasswordHash)))
            {
                logger.LogError(
                    "Identity repair: user '{Email}' has a corrupted PasswordHash and cannot sign in. " +
                    "Delete this account or reset its password (e.g. via SQL or the register endpoint).",
                    user.Email);
            }
        }

        private static async Task EnsureSeedUserAsync(
            IdentityContext db, ILogger logger, PasswordHasher<ApplicationUser> hasher,
            string email, string userName, string password, string seedId)
        {
            var normalized = email.ToUpperInvariant();
            var user = await db.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == normalized);

            if (user == null)
            {
                var created = new ApplicationUser
                {
                    Id = seedId,
                    UserName = userName,
                    Email = email,
                    NormalizedEmail = normalized,
                    NormalizedUserName = userName.ToUpperInvariant(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };
                created.PasswordHash = hasher.HashPassword(created, password);
                db.Users.Add(created);
                await db.SaveChangesAsync();
                logger.LogWarning("Identity repair: seeded user '{Email}' was missing and has been recreated with the default password.", email);
                return;
            }

            if (!IsValidV3Hash(user.PasswordHash))
            {
                user.PasswordHash = hasher.HashPassword(user, password);
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.ConcurrencyStamp = Guid.NewGuid().ToString();
                await db.SaveChangesAsync();
                logger.LogWarning(
                    "Identity repair: PasswordHash of '{Email}' was corrupted (invalid base-64 / format) and has been " +
                    "reset to the default seed password ({Password}). Change it after signing in.",
                    email, password);
            }
        }

        /// <summary>ASP.NET Core Identity V3 hashes: 0x01 marker + PRF/iterations/lengths header (>= 30 bytes).</summary>
        private static bool IsValidV3Hash(string? hash)
        {
            if (string.IsNullOrWhiteSpace(hash) || hash.Length < 20)
            {
                return false;
            }
            try
            {
                var bytes = Convert.FromBase64String(hash);
                return bytes.Length >= 30 && bytes[0] == 0x01;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

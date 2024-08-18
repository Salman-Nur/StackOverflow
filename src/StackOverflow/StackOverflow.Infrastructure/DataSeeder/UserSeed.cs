using Microsoft.AspNetCore.Identity;
using StackOverflow.Infrastructure.Membership;

namespace StackOverflow.Infrastructure.DataSeeder
{
    public static class UserSeed
    {
        public static IList<ApplicationUser> Users()
        {
            var passHasher = new PasswordHasher<ApplicationUser>();

            var user1 = new ApplicationUser()
            {
                Id = new Guid("42AF1478-6EF5-494E-AA5F-9E99B959DACC"),
                UserName = "salman",
                NormalizedUserName = "SALMAN.INSTALLER",
                Email = "salman.installer@gmail.com",
                NormalizedEmail = "SALMAN.INSTALLER@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "FLNGVXFQGWE6JKWCRVP3664L4ESTP4VU",
                LockoutEnabled = true,
            };

            user1.PasswordHash = passHasher.HashPassword(user1, "123456");


            var user2 = new ApplicationUser()
            {
                Id = new Guid("2DC6FB4F-3229-491F-BACB-0EB9926052C6"),
                UserName = "hasan",
                NormalizedUserName = "SALMAN.QUBIT",
                Email = "salman.qubit@gmail.com",
                NormalizedEmail = "SALMAN.QUBIT@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = "SVSGSD3LW72US32MCRKAL5GMUUL23YIB",
                LockoutEnabled = true,
            };

            user2.PasswordHash = passHasher.HashPassword(user2, "123456");

            var users = new List<ApplicationUser>() { user1, user2 };

            return users;
        }
    }
}

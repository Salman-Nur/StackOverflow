
using StackOverflow.Infrastructure.Membership;

namespace StackOverflow.Infrastructure.DataSeeder
{
    public static class UserClaimSeed
    {
        public static IList<ApplicationUserClaim> Claims()
        {
            var claims = new List<ApplicationUserClaim>()
            {
                new ApplicationUserClaim()
                {
                    Id = 1,
                    UserId = new Guid("42AF1478-6EF5-494E-AA5F-9E99B959DACC"),
                    ClaimType = "PostQuestion",
                    ClaimValue = "true"
                },
                new ApplicationUserClaim()
                {
                    Id = 2,
                    UserId = new Guid("42AF1478-6EF5-494E-AA5F-9E99B959DACC"),
                    ClaimType = "PostAnswer",
                    ClaimValue = "true"
                },
                new ApplicationUserClaim()
                {
                    Id = 3,
                    UserId = new Guid("2DC6FB4F-3229-491F-BACB-0EB9926052C6"),
                    ClaimType = "PostQuestion",
                    ClaimValue = "true"
                },
                new ApplicationUserClaim()
                {
                    Id = 4,
                    UserId = new Guid("2DC6FB4F-3229-491F-BACB-0EB9926052C6"),
                    ClaimType = "PostAnswer",
                    ClaimValue = "true"
                }
            };

            return claims;
        }
    }
}


using StackOverflow.Domain.Entities;

namespace StackOverflow.DataSeeder
{
    public static class UserProfileSeed
    {
        public static IList<User> UserProfiles()
        {
            var user1Profile = new User
            {
                Id = new Guid("42AF1478-6EF5-494E-AA5F-9E99B959DACC"),
                UserName = "salman",
                Title = "Student",
                Description = "Studying at Department of Mathematics Islamic University, Kushtia-7003, Bangladesh",
                Country = "Bangladesh",
                ImageUrl = "images/5b7f4f24-f43d-45d1-b0eb-d6c41d5dde95_salman1.jpg"
            };
            var user2Profile = new User
            {
                Id = new Guid("2DC6FB4F-3229-491F-BACB-0EB9926052C6"),
                UserName = "hasan",
                Title = "Student",
                Description = "Studying at Department of CSE Islamic University, Kushtia-7003, Bangladesh",
                Country = "Bangladesh",
                ImageUrl = "images/72b73e67-2e3d-4088-9c09-dbdbabd17eb5_salman2.jpg"
            };

            var userProfiles = new List<User> { user1Profile, user2Profile };

            return userProfiles;
        }
    }
}

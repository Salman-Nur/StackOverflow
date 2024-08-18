using Microsoft.AspNetCore.Identity;
using System;

namespace StackOverflow.Infrastructure.Membership
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? DisplayName { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
    }
}

using Microsoft.AspNetCore.Authorization;
using StackOverflow.Infrastructure.Membership.Requirements;
using StackOverflow.Infrastructure.Membership;

namespace StackOverflow.Infrastructure.Membership.Requirements
{
    public class AnswerPostRequirementHandler :
          AuthorizationHandler<AnswerPostRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               AnswerPostRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "PostAnswer" && x.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

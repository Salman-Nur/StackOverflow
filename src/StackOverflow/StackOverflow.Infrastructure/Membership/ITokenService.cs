
using System.Security.Claims;

namespace StackOverflow.Infrastructure.Membership
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims, string key, string issuer, string audience);
    }
}
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Shopper.WebApi.AuthenticationHandlers
{

    public interface IAuthorizeRepository
    {
        bool Authorize(string secretKey);
    }

    public class FakeAuthorizeRepository : IAuthorizeRepository
    {
        public bool Authorize(string secretKey)
        {
            return secretKey == "123";
        }
    }

    public class SecretKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthorizeRepository authorizeRepository;

        public SecretKeyAuthenticationHandler(
            IAuthorizeRepository authorizeRepository,
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this.authorizeRepository = authorizeRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.TryGetValue("secret-key", out var secretKey))
            {
                if (authorizeRepository.Authorize(secretKey))
                {
                    ClaimsIdentity identity = new ClaimsIdentity("SecretKey");

                    Claim claim1 = new Claim("kat", "B");
                    Claim claim2 = new Claim("kat", "C");
                    Claim claim3 = new Claim(ClaimTypes.Role, "developer");
                    Claim claim4 = new Claim(ClaimTypes.Role, "trainer");

                    identity.AddClaim(claim1);
                    identity.AddClaim(claim2);
                    identity.AddClaim(claim3);

                    identity.AddClaim(new Claim(ClaimTypes.Email, "marcin.sulecki@sulmar.pl"));
                    identity.AddClaim(new Claim(ClaimTypes.MobilePhone, "555851649"));

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    AuthenticationTicket ticket = new AuthenticationTicket(principal, "SecretKey");

                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("Invalid secret-key");
                }
            }
            else
            {
                return AuthenticateResult.Fail("Missing secret-key");
            }
        }
    }
}

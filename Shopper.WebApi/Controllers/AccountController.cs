using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shopper.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.WebApi.Controllers
{

    public interface ITokenService
    {
        SecurityToken Create(ApplicationUser user);
    }


    // dotnet add package System.IdentityModel.Tokens.Jwt
    public class JwtTokenService : ITokenService
    {
        public SecurityToken Create(ApplicationUser user)
        {
            string issuer = "santander";

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            string secretKey = "your-256-bit-secret";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer,
                issuer,
                claims,                
                expires: DateTime.Now.AddMinutes(30), 
                signingCredentials: credentials);

            return securityToken;
        }
    }

    public class ApplicationUser : IdentityUser
    {
        public string Account { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .Property(p => p.Account)
                .HasMaxLength(20);

            base.OnModelCreating(builder);
        }
    }

    [Authorize]
    public class AccountController : ControllerBase
    {
        // dotnet add package Microsoft.AspNetCore.Identity
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService tokenService;

        public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        // POST api/token
        // { email : hasło }

        [AllowAnonymous]
        [HttpPost("api/token")]
        public async Task<ActionResult<string>> CreateTokenAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                var isValid = await userManager.CheckPasswordAsync(user, model.Password);

                if (isValid)
                {
                    var securityToken = tokenService.Create(user);

                    string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                    // optionalne: zapis tokena do ciasteczka
                    Response.Cookies.Append("X-Access-Token", token, new CookieOptions { SameSite = SameSiteMode.Strict });

                    return Ok(token);
                }
            }

            return Unauthorized();
        }
    }
}

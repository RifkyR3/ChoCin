using ChoCin.Entities;
using ChoCin.Server.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChoCin.Server.Services
{
    public class AuthService
    {
        public const string CookieAuthenticationScheme = "aes_login_cookie";
        public const string JwtToken = "JwtToken";
        public const string Dual = CookieAuthenticationScheme + "," + JwtToken;

        private readonly ChocinDbContext _context;

        public AuthService(ChocinDbContext context)
        {
            this._context = context;
        }

        public async Task<LoginFormModel> GetUser(string username)
        {
            var user = await this._context
                .CUsers
                .AsNoTracking()
                .Where(q => q.UserName == username)
                .Select( q=> new LoginFormModel
                {
                    UserId = q.UserId,
                    Username = q.UserName,
                    Password = q.UserPassword
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public bool CheckingPassword(string enteredPasword, string hashedPassword)
        {

            bool valid = BCrypt.Net.BCrypt.Verify(enteredPasword, hashedPassword);

            return valid;
        }

        public ClaimsPrincipal GenerateClaims(LoginFormModel model)
        {
            var claims = new ClaimsIdentity(CookieAuthenticationScheme);

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Name, model.FullName));
            /*claims.AddClaim(new Claim(ClaimTypes.Email, model.Email));
            claims.AddClaim(new Claim(ClaimTypes.Role, model.Role));*/

            return new ClaimsPrincipal(claims);
        }
    }
}

using ChoCin.Entities;
using ChoCin.Server.Models;
using ChoCin.Server.Models.Auth;
using ChoCin.Server.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChoCin.Server.Services
{
    public class AuthService
    {
        private readonly ChocinDbContext _context;
        private readonly AppSettings _appSettings;

        public AuthService(ChocinDbContext context, IOptions<AppSettings> appSettings)
        {
            this._context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<JwtAuthResponse?> Authenticate(JwtLoginFormModel model)
        {
            var tmpUser = await this._context
                .CUsers
                .AsNoTracking()
                .Where(q => q.UserName == model.UserName)
                .FirstOrDefaultAsync();

            if (tmpUser == null) { return null; }

            var validatePassword = this.CheckingPassword(model.Password, tmpUser.UserPassword);

            // return null if user not found
            if (!validatePassword) return null;

            var user = new UserModel
            {
                UserFullName = tmpUser.UserFullName,
                UserName = tmpUser.UserName,
                UserPassword = tmpUser.UserPassword,
                UserId = tmpUser.UserId
            };

            // authentication successful so generate jwt token
            var token = await generateJwtToken(user);

            return new JwtAuthResponse
            {
                FullName = tmpUser.UserFullName,
                Username = tmpUser.UserName,
                Id = tmpUser.UserId,
                Token = token
            };
        }

        // helper methods
        private async Task<string> generateJwtToken(UserModel user)
        {
            //Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }

        private bool CheckingPassword(string enteredPasword, string hashedPassword)
        {
            bool valid = BCrypt.Net.BCrypt.Verify(enteredPasword, hashedPassword);

            return valid;
        }
    }
}
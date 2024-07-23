using ChoCin.Entities;
using ChoCin.Server.Models;
using ChoCin.Server.Models.Auth;
using ChoCin.Server.Models.Group;
using ChoCin.Server.Models.Module;
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
        private readonly ModuleService _moduleService;

        public AuthService(ChocinDbContext context, IOptions<AppSettings> appSettings)
        {
            this._context = context;
            _appSettings = appSettings.Value;
            this._moduleService = new ModuleService(this._context);
        }

        public async Task<JwtAuthResponse?> Authenticate(JwtLoginFormModel model)
        {
            var tmpUser = await this._context
                .CUsers
                .AsNoTracking()
                .Where(q => q.Username == model.UserName)
                .FirstOrDefaultAsync();

            if (tmpUser == null) { return null; }

            var validatePassword = this.CheckingPassword(model.Password, tmpUser.UserPassword);

            // return null if user not found
            if (!validatePassword) return null;

            // authentication successful so generate jwt token
            var token = await generateJwtToken(tmpUser.UserId);

            JwtAuthResponse response = await this._context
                .CUsers
                .AsNoTracking()
                .Where(W => W.UserId == tmpUser.UserId)
                .Select(Q => new JwtAuthResponse
                {
                    Id = Q.UserId,
                    Username = Q.Username,
                    FullName = Q.UserFullName,
                    Token = token,
                    Groups = Q.Groups
                        .Select(G => new GroupModel
                        {
                            GroupId = G.GroupId,
                            GroupName = G.GroupName,
                        }).ToList()
                }).FirstAsync();

            if(response.Groups?.Count > 0)
            {
                List<ModuleModel> modules = await this._moduleService.GetModuleByGroup(response.Groups.Select(G => G.GroupId).First());
                response.Modules = modules;
            }

            return response;
        }

        // helper methods
        private async Task<string> generateJwtToken(Guid userId)
        {
            //Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
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
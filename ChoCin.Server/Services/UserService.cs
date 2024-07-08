using ChoCin.Entities;
using ChoCin.Server.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ChoCin.Server.Services
{
    public class UserService
    {
        private protected ChocinDbContext dbContext;
        public UserService(ChocinDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            return await this.dbContext.CUsers.Select(
                Q => new UserModel
                {
                    UserFullName = Q.UserFullName,
                    UserId = Q.UserId,
                    UserName = Q.UserName,
                }).ToListAsync();
        }

        public async Task<UserModel?> GetUserById(int id)
        {
            return await this.dbContext.CUsers.Select(
                Q => new UserModel
                {
                    UserFullName = Q.UserName,
                    UserId = Q.UserId,
                    UserName = Q.UserName
                }).Where(q => q.UserId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddUser(AddUpdateUser user)
        {
            var add = new CUser()
            {
                UserName = user.UserName,
                UserPassword = BCrypt.Net.BCrypt.HashPassword(user.Password),
                UserFullName = user.Name
            };

            this.dbContext.CUsers.Add(add);
            var result = await dbContext.SaveChangesAsync();
            return result >= 0;
        }

        public async Task<bool> UpdateUser(int id, AddUpdateUser updateUser)
        {
            var user = await this.dbContext.CUsers.Where(q => q.UserId == id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.UserName = updateUser.UserName;
                user.UserPassword = BCrypt.Net.BCrypt.HashPassword((string)updateUser.Password);
                user.UserFullName = updateUser.Name;
                var result = await dbContext.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await this.dbContext.CUsers.Where(q => q.UserId == id).FirstOrDefaultAsync();
            if (user != null)
            {
                this.dbContext.Remove(user);
                var result = await dbContext.SaveChangesAsync();
                return result >= 0;
            }

            return false;
        }
    }
}

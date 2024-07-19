using ChoCin.Entities;
using ChoCin.Server.Models.Group;
using ChoCin.Server.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ChoCin.Server.Services
{
    public class UserService
    {
        protected ChocinDbContext dbContext;

        public UserService(ChocinDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            return await this.dbContext
                .CUsers
                .AsNoTracking()
                .Select(
                Q => new UserModel
                {
                    UserFullName = Q.UserFullName,
                    UserId = Q.UserId,
                    UserName = Q.UserName,
                    Groups = Q.Groups.Select(G => new GroupModel
                    {
                        GroupId = G.GroupId,
                        GroupName = G.GroupName,
                    }).ToList(),
                }).ToListAsync();
        }

        public async Task<UserModel?> GetUserById(int id)
        {
            return await this.dbContext
                .CUsers
                .AsNoTracking()
                .Select(
                Q => new UserModel
                {
                    UserFullName = Q.UserName,
                    UserId = Q.UserId,
                    UserName = Q.UserName,
                    Groups = Q.Groups.Select(G => new GroupModel
                    {
                        GroupId = G.GroupId,
                        GroupName = G.GroupName,
                    }).ToList(),
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

            if (user.Groups?.Count > 0)
            {
                var groups = await this.dbContext
                .CGroups
                .Where(Q => user.Groups.Select(ug => ug.GroupId).Contains(Q.GroupId))
                .ToListAsync();

                add.Groups = groups;
            }

            this.dbContext.CUsers.Add(add);
            var result = await dbContext.SaveChangesAsync();
            return result >= 0;
        }

        public async Task<bool> UpdateUser(int id, AddUpdateUser updateUser)
        {
            var user = await this.dbContext
                .CUsers
                .Include(u => u.Groups)
                .Where(q => q.UserId == id)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                user.UserName = updateUser.UserName;
                user.UserPassword = BCrypt.Net.BCrypt.HashPassword((string)updateUser.Password);
                user.UserFullName = updateUser.Name;

                if(user.Groups?.Count > 0) {
                    user.Groups.Clear();
                }
                
                if (updateUser.Groups?.Count > 0)
                {
                    var groups = await this.dbContext
                    .CGroups
                    .Where(Q => updateUser.Groups.Select(ug => ug.GroupId).Contains(Q.GroupId))
                    .ToListAsync();

                    user.Groups = groups;
                }

                dbContext.CUsers.Update(user);
                var result = await dbContext.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await this.dbContext
                .CUsers
                .AsNoTracking()
                .Where(q => q.UserId == id)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                this.dbContext.CUsers.Remove(user);
                var result = await dbContext.SaveChangesAsync();
                return result >= 0;
            }

            return false;
        }
    }
}
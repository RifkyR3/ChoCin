using ChoCin.Entities;
using ChoCin.Server.Models;
using ChoCin.Server.Models.Group;
using Microsoft.EntityFrameworkCore;

namespace ChoCin.Server.Services
{
    public class GroupService
    {
        protected ChocinDbContext dbContext;

        public GroupService(ChocinDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GroupModel>> GetGroups()
        {
            return await this.dbContext
                .CGroups
                .AsNoTracking()
                .OrderBy(O => O.GroupId)
                .Select(Q => new GroupModel
                {
                    GroupId = Q.GroupId,
                    GroupName = Q.GroupName
                })
                .ToListAsync();
        }

        public async Task<GroupModel?> GetGroupById(int id)
        {
            return await this.dbContext
                .CGroups
                .AsNoTracking()
                .Where(W => W.GroupId == id)
                .OrderBy(O => O.GroupId)
                .Select(Q => new GroupModel
                {
                    GroupId = Q.GroupId,
                    GroupName = Q.GroupName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddGroup(AddUpdateGroup add)
        {
            var obj = new CGroup
            {
                GroupName = add.GroupName,
            };

            this.dbContext.CGroups.Add(obj);
            var result = await dbContext.SaveChangesAsync();
            return result >= 0;
        }

        public async Task<bool> UpdateGroup(int id, AddUpdateGroup update)
        {
            var group = await this.dbContext
                .CGroups
                .AsNoTracking()
                .Where(q => q.GroupId == id)
                .FirstOrDefaultAsync();

            if (group != null)
            {
                group.GroupName = update.GroupName;
                var result = await dbContext.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }

        public async Task<bool> DeleteGroup(int id)
        {
            var group = await this.dbContext
                .CGroups
                .AsNoTracking()
                .Where(q => q.GroupId == id)
                .FirstOrDefaultAsync();

            if (group != null)
            {
                this.dbContext.Remove(group);
                var result = await dbContext.SaveChangesAsync();
                return result >= 0;
            }

            return false;
        }

        public async Task<List<DropDownModel>> GetComboGroup()
        {
            return await this.dbContext
                .CGroups
                .AsNoTracking()
                .OrderBy(O => O.GroupId)
                .Select(Q => new DropDownModel
                {
                    Id = Q.GroupId,
                    Name = Q.GroupName
                })
                .ToListAsync();
        }
    }
}
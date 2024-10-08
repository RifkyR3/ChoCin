﻿using ChoCin.Entities;
using ChoCin.Server.Models;
using ChoCin.Server.Models.Form;
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

        public async Task<GroupModel?> GetGroupById(Guid id)
        {
            return await this.dbContext
                .CGroups
                .AsNoTracking()
                .Where(W => W.GroupId == id)
                .OrderBy(O => O.GroupId)
                .Select(Q => new GroupModel
                {
                    GroupId = Q.GroupId,
                    GroupName = Q.GroupName,
                    GroupModuleIds = Q.Modules.Select(QM => QM.ModuleId).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddGroup(AddUpdateGroup add)
        {
            var obj = new CGroup
            {
                GroupId = Guid.NewGuid(),
                GroupName = add.GroupName,
            };

            if (add.ModuleIds != null && add.ModuleIds.Count > 0)
            {
                var modules = await this.dbContext
                .CModules
                .Where(Q => add.ModuleIds.Contains(Q.ModuleId))
                .ToListAsync();

                obj.Modules = modules;
            }

            this.dbContext.CGroups.Add(obj);
            var result = await dbContext.SaveChangesAsync();
            return result >= 0;
        }

        public async Task<bool> UpdateGroup(Guid id, AddUpdateGroup update)
        {
            var group = await this.dbContext
                .CGroups
                .Include(G => G.Modules)
                .Where(Q => Q.GroupId == id)
                .FirstOrDefaultAsync();

            if (group != null)
            {
                group.GroupName = update.GroupName;

                if (group.Modules?.Count > 0)
                {
                    group.Modules.Clear();
                }

                if (update.ModuleIds?.Count > 0)
                {
                    var modules = await this.dbContext
                    .CModules
                    .Where(Q => update.ModuleIds.Contains(Q.ModuleId))
                    .ToListAsync();

                    group.Modules = modules;
                }

                this.dbContext.CGroups.Update(group);
                var result = await dbContext.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }

        public async Task<bool> DeleteGroup(Guid id)
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
                .OrderBy(O => O.GroupName)
                .Select(Q => new DropDownModel
                {
                    Id = Q.GroupId,
                    Name = Q.GroupName
                })
                .ToListAsync();
        }
    }
}
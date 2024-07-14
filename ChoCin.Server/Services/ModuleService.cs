using ChoCin.Entities;
using ChoCin.Server.Models.Module;
using Microsoft.EntityFrameworkCore;

namespace ChoCin.Server.Services
{
    public class ModuleService
    {
        protected ChocinDbContext dbContext;

        public ModuleService(ChocinDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ModuleModel>> GetModuleByGroup(int groupId)
        {
            return await this.dbContext
                .CModules
                .AsNoTracking()
                .Where(W =>
                    W.ModuleSubId == null
                    && W.Groups.Select(WG => WG.GroupId).Contains(groupId)
                )
                .OrderBy(O => O.ModuleOrder)
                .Select(Q => new ModuleModel
                {
                    Id = Q.ModuleId,
                    Name = Q.ModuleName,
                    Icon = Q.ModuleIcon,
                    Path = Q.ModulePath,
                    Children = Q.InverseModuleSub
                        .OrderBy(OC => OC.ModuleOrder)
                        .Select(QC => new ModuleModel
                        {
                            Id = QC.ModuleId,
                            Name = QC.ModuleName,
                            Icon = QC.ModuleIcon,
                            Path = QC.ModulePath,
                        }).ToList()
                })
                .ToListAsync();
        }
    }
}
using ChoCin.Server.Helpers;
using ChoCin.Server.Models.Module;
using ChoCin.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChoCin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModuleController : ControllerBase
    {
        private readonly ModuleService _moduleService;

        public ModuleController(ModuleService moduleService)
        {
            this._moduleService = moduleService;
        }

        [HttpGet]
        [Route("{groupId}", Name = "getModuleByGroup")]
        public async Task<ActionResult<List<ModuleModel>>> GetModuleByGroup([FromRoute] int groupId)
        {
            return await _moduleService.GetModuleByGroup(groupId);
        }
    }
}
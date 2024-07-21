using ChoCin.Server.Helpers;
using ChoCin.Server.Models;
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

        [HttpGet(Name = "getListModule")]
        public async Task<ActionResult<List<ModuleModel>>> GetListModule()
        {
            return await _moduleService.GetModules();
        }

        [HttpGet("{id}", Name = "getModuleById")]
        public async Task<ActionResult<ModuleModel>> GetModuleById(Guid id)
        {
            var user = await _moduleService.GetModuleById(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost(Name = "addModule")]
        public async Task<IActionResult> AddModule([FromBody] AddUpdateModule addModule)
        {
            if (!await _moduleService.AddModule(addModule))
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Module Created Successfully"
            });
        }

        [HttpPut]
        [Route("{id}", Name = "updateModule")]
        public async Task<IActionResult> UpdateModule([FromBody] AddUpdateModule updateModule, [FromRoute] Guid id)
        {
            if (!await _moduleService.UpdateModule(id, updateModule))
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Module Updated Successfully"
            });
        }

        [HttpDelete]
        [Route("{id}", Name = "deleteModule")]
        public async Task<IActionResult> DeleteModule([FromRoute] Guid id)
        {
            if (!await _moduleService.DeleteModule(id))
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Module Deleted Successfully"
            });
        }

        [HttpGet]
        [Route("getModuleByGroup/{groupId}", Name = "getModuleByGroup")]
        public async Task<ActionResult<List<ModuleModel>>> GetModuleByGroup([FromRoute] Guid groupId)
        {
            return await _moduleService.GetModuleByGroup(groupId);
        }

        [HttpGet]
        [Route("getComboMainModule", Name = "getComboMainModule")]
        public async Task<ActionResult<List<DropDownModel>>> GetComboMainModule()
        {
            return await _moduleService.GetComboMainModule();
        }

        [HttpGet]
        [Route("getModuleTree", Name = "getModuleTree")]
        public async Task<ActionResult<List<ModuleModel>>> GetModuleTree()
        {
            return await _moduleService.GetModuleTree();
        }
    }
}
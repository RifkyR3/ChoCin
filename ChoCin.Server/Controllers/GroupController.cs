using ChoCin.Server.Helpers;
using ChoCin.Server.Models;
using ChoCin.Server.Models.Group;
using ChoCin.Server.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChoCin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            this._groupService = groupService;
        }

        [HttpGet(Name = "GetListGroup")]
        public async Task<ActionResult<List<GroupModel>>> Get()
        {
            return await this._groupService.GetGroups();
        }

        [HttpGet("{id}", Name = "GetGroupById")]
        public async Task<ActionResult<GroupModel>> Get(int id)
        {
            var group = await this._groupService.GetGroupById(id);
            if (group == null)
            {
                return new NotFoundResult();
            }
            return group;
        }

        [HttpPost(Name = "AddGroup")]
        public async Task<IActionResult> Post([FromBody] AddUpdateGroup value)
        {
            if (!await _groupService.AddGroup(value))
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Group Created Successfully"
            });
        }

        [HttpPut("{id}", Name = "UpdateGroup")]
        public async Task<IActionResult> Put(int id, [FromBody] AddUpdateGroup value)
        {
            if (!await _groupService.UpdateGroup(id, value))
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Group Updated Successfully"
            });
        }

        [HttpDelete("{id}", Name = "DeleteGroup")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _groupService.DeleteGroup(id))
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Group Deleted Successfully"
            });
        }

        [HttpGet("/combo-group", Name = "GetComboGroup")]
        public async Task<ActionResult<List<DropDownModel>>> GetComboGroup()
        {
            return await _groupService.GetComboGroup();
        }
    }
}
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

        [HttpGet(Name = "getListGroup")]
        public async Task<ActionResult<List<GroupModel>>> GetListGroup()
        {
            return await this._groupService.GetGroups();
        }

        [HttpGet("{id}", Name = "getGroupById")]
        public async Task<ActionResult<GroupModel>> GetGroupById(int id)
        {
            var group = await this._groupService.GetGroupById(id);
            if (group == null)
            {
                return new NotFoundResult();
            }
            return group;
        }

        [HttpPost(Name = "addGroup")]
        public async Task<IActionResult> AddGroup([FromBody] AddUpdateGroup value)
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

        [HttpPut("{id}", Name = "updateGroup")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] AddUpdateGroup value)
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

        [HttpDelete("{id}", Name = "deleteGroup")]
        public async Task<IActionResult> DeleteGroup(int id)
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

        [HttpGet("getComboGroup",Name = "getComboGroup")]
        public async Task<ActionResult<List<DropDownModel>>> GetComboGroup()
        {
            return await _groupService.GetComboGroup();
        }
    }
}
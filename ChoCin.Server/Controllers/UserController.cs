using ChoCin.Server.Helpers;
using ChoCin.Server.Models.User;
using ChoCin.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChoCin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            this._userService = userService;
        }

        [HttpGet(Name = "getListUser")]
        public async Task<ActionResult<List<UserModel>>> GetListUser()
        {
            return await _userService.GetUsers();
        }

        [HttpGet("{id}", Name = "getUserById")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost(Name = "addUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUpdateUser addUser)
        {
            if (!await _userService.AddUser(addUser))
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "User Created Successfully"
            });
        }

        [HttpPut]
        [Route("{id}", Name = "updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] AddUpdateUser updateUser, [FromRoute] int id)
        {
            if (!await _userService.UpdateUser(id, updateUser))
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "User Updated Successfully"
            });
        }

        [HttpDelete]
        [Route("{id}", Name = "deleteUser")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!await _userService.DeleteUser(id))
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "User Deleted Successfully"
            });
        }
    }
}
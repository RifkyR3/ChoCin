using ChoCin.Server.Models.User;
using ChoCin.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChoCin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            this._userService = userService;
        }

        [HttpGet(Name = "GetListUser")]
        public async Task<ActionResult<List<UserModel>>> Get()
        {
            return await _userService.GetUsers();
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost(Name = "AddUser")]
        public async Task<IActionResult> Post([FromBody] AddUpdateUser addUser)
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
        [Route("{id}", Name = "UpdateUser")]
        public async Task<IActionResult> Put([FromBody] AddUpdateUser updateUser, [FromRoute] int id)
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
        [Route("{id}", Name = "DeleteUser")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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

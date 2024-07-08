using ChoCin.Server.Models.Auth;
using ChoCin.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChoCin.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<JwtAuthResponse>> Authenticate(JwtLoginFormModel model)
        {
            var response = await _authService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return response;
        }
    }
}

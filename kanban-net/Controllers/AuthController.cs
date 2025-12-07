using kanban_net.DTOs;
using kanban_net.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kanban_net.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(AuthService service) : ControllerBase
    {
        private readonly AuthService _service = service;

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO data)
        {
            TokenDTO token = _service.Register(data);

            return Ok(token);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO data)
        {
            TokenDTO token = _service.Login(data);

            return Ok(token);
        }
    }
}
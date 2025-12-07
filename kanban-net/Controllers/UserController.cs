using kanban_net.DTOs;
using kanban_net.Models;
using kanban_net.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kanban_net.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController(UserService service) : ControllerBase
    {
        private readonly UserService _service = service;

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public IActionResult Create(UserCreateDTO data)
        {
            UserDTO user = _service.Create(data);

            return Created(new Uri($"http://localhost:3000/api/users/{user.Id}"), user);
        }

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            UserDTO user = _service.Get(id);

            return Ok(user);
        }

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpGet]
        public IActionResult GetAll()
        {
            UserDTO[] users = _service.GetAll();

            return Ok(users);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPatch("{id}")]
        public IActionResult Update(long id, UserUpdateDTO data)
        {
            UserDTO user = _service.Update(id, data);

            return Ok(user);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _service.Delete(id);

            return NoContent();
        }
    }
}
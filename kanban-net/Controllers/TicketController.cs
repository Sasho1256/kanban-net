using System.Security.Claims;
using kanban_net.DTOs;
using kanban_net.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static kanban_net.Models.User;

namespace kanban_net.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    [Authorize]
    public class TicketController(TicketService service) : ControllerBase
    {
        private readonly TicketService _service = service;

        [HttpPost]
        public IActionResult Create(TicketCreateDTO data)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            TicketDTO ticket = _service.Create(data, currentUserId);

            return Created(new Uri($"http://localhost:3000/api/tickets/{ticket.Id}"), ticket);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            TicketDTO ticket = _service.Get(id);

            return Ok(ticket);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            TicketDTO[] tickets = _service.GetAll();

            return Ok(tickets);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(long id, TicketUpdateDTO data)
        {
            var currentUserRole = User.FindFirst(ClaimTypes.Role)!.Value;

            Enum.TryParse<UserRole>(currentUserRole, true, out var r);

            TicketDTO ticket = _service.Update(id, data, r);

            return Ok(ticket);
        }

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _service.Delete(id);

            return NoContent();
        }
    }
}
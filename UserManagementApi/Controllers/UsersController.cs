using Microsoft.AspNetCore.Mvc;
using System;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users?status=Active
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? status)
        {
            var users = _userService.GetAll(status);
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var createdUser = _userService.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/users/{id}/status
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(Guid id, [FromBody] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest("Status is required");
            }

            var updated = _userService.UpdateStatus(id, status);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

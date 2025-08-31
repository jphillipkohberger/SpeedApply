using Microsoft.AspNetCore.Mvc;
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;

namespace SpeedApply.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // GET api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<UsersDto>>> GetUsers(int id)
        {
            var users = await _userService.GetUsersAsync();
            if (users == null) return NotFound();
            return Ok(users);
        }

        // GET api/<UsersController>/create/5
        [HttpGet("Create/{id}")]
        public async Task<ActionResult<UsersDto>> CreateUser(int id)
        {
            var user = await _userService.CreateUserAsync();
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}

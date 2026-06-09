using Microsoft.AspNetCore.Mvc;
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace SpeedApply.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly IFilesService _fileService;

        public UsersController(IUsersService userService, IFilesService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdWithQueriesFilesAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUserWithQueries/{id}")]
        public async Task<ActionResult<UsersDto>> GetUserWithQueries(int id)
        {
            var user = await _userService.GetUserByIdWithQueriesAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUserWithFiles/{id}")]
        public async Task<ActionResult<UsersDto>> GetUserWithFiles(int id)
        {
            var user = await _userService.GetUserByIdWithFilesAsync(id);
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

        // POST api/<UsersController>/Create
        [HttpPost("Create")]
        public async Task<ActionResult<UsersDto>> CreateUser([FromBody] UsersDto usersDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.CreateUserAsync(usersDto);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST api/<UsersController>/SaveProfile
        [HttpPost("SaveProfile")]
        public async Task<ActionResult<UsersDto>> SaveProfile([FromForm] AddressDto addressDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /**
             * save address
             */
            string address = $"{addressDto.Street}, {addressDto.City}, {addressDto.State} {addressDto.Zip}";
            var user = await _userService.SaveUserProfileAsync(addressDto.UserId, address, addressDto.MinSal);
            if (user == null) return NotFound();

            /**
             * save incoming file
             */
            string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            string safeFileName = Path.GetRandomFileName() + ".pdf";
            string filePath = Path.Combine(targetDirectory, safeFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await addressDto.Resume.CopyToAsync(fileStream);
            }

            /**
             * save file metadata in db
             */
            var file = await _fileService.SaveUserFileAsync(addressDto.UserId, filePath);
            if (file == null) return NotFound();

            return Ok(user);
        }

        // POST api/<UsersController>/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UsersDto>> Login([FromBody] UsersDto usersDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.LoginUserAsync(usersDto);
            if (user == null) return NotFound();

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // Create identity & principal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Sign in with cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Ok(user);
        }

        // GET api/<UsersController>/Logout
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Logged out");
        }
    }
}

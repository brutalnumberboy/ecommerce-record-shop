using API.Authorization;
using API.Mapping.DTO;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly TokenService _tokenService;

        public AuthorizationController(UserManager<User> userManager, ILogger<AuthorizationController> logger, TokenService tokenService)
        {
            _userManager = userManager;
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User { UserName = register.UserName.Trim(), Email = register.Email };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Error Code: {error.Code}, Error Description: {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }

            var token = _tokenService.NewToken(user);
            user.Token = token;
            await _userManager.UpdateAsync(user);

            return new UserDTO { UserName = user.UserName, Email = user.Email, Token = token };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }

            var result = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }

            var token = _tokenService.NewToken(user);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(30),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("jwt", token, cookieOptions);

            user.Token = token;
            await _userManager.UpdateAsync(user);

            return new UserDTO { UserName = user.UserName, Email = user.Email, Token = token };
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var email = User.FindFirstValue("email");
            if (email == null)
            {
                return Unauthorized("Email claim missing.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
        
            Response.Cookies.Delete("jwt");

            user.Token = null;
            await _userManager.UpdateAsync(user);

            return Ok("Logged out successfully.");
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue("email");
            if (email == null)
            {
                return Unauthorized("Email claim missing.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return new UserDTO { UserName = user.UserName, Email = user.Email, Token = user.Token! };
        }
    }
}
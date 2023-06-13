using consist.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace consist.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuth _authService;

        public AuthController(ILogger<AuthController> logger, IAuth authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken(string username, string password = "123")
        {
            try
            {
                String token = await _authService.CreateToken(null, password);

                return Ok(token);
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogWarning($"GetToken failed, invalid username or password for user {username}");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetToken failed {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}

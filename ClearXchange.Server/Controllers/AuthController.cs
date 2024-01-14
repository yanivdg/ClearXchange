using ClearXchange.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearXchange.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet("generate-token")]
        [AllowAnonymous] // Allow access without authentication
        public IActionResult GenerateToken()
        {
            // For demonstration purposes, generate a token without user authentication
            var token = _jwtService.GenerateToken("SampleUser", "UserRole");

            // Return the generated token in the response
            return Ok(new { Token = token });
        }
    }


}

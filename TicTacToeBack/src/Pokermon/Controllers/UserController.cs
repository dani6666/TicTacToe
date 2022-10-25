using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Core.Interfaces.Services;
using TicTacToe.Core.Model;

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            var userId = await _userService.Authenticate(userCredentials);

            if (!userId.HasValue)
                return Unauthorized();

            return Ok(userId.Value);
        }
    }
}

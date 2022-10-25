using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Core.Interfaces.Services;
using TicTacToe.Core.Model;
using TicTacToe.Core.Model.Enums;

namespace TicTacToe.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IUserService _userService;

        public GameController(IGameService gameService, IUserService userService)
        {
            _gameService = gameService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public GameState Index(int id, [FromHeader] Guid playerId)
        {
            return _gameService.GetGame(id, playerId);
        }

        [HttpPost]
        public async Task<IActionResult> JoinGame([FromHeader] Guid playerId)
        {
            if (!await _userService.ValidatePlayerId(playerId))
                return Unauthorized();

            var gameId = _gameService.GetOrCreateGame(playerId);

            return Ok(gameId);
        }

        [HttpPost("fold/{id}")]
        public ActionResult MakeMove(int id, [FromHeader] Guid playerId, [FromBody] Move move)
        {
            var moveResult = _gameService.MakeMove(id, playerId, move.X, move.Y);

            return moveResult switch
            {
                MoveResult.InvalidMove => BadRequest(),
                MoveResult.NotFoundError => NotFound(),
                _ => Ok()
            };
        }
    }
}

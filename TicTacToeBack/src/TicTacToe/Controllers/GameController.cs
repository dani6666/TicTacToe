using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Core.Interfaces.Services;
using TicTacToe.Core.Model;
using TicTacToe.Core.Model.Enums;

namespace TicTacToe.Controllers
{
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
        public ActionResult<GameStateResponse> Index(int id, [FromHeader] Guid playerId)
        {
            var gameState = _gameService.GetGame(id, playerId);

            if (gameState == null)
                return NotFound();

            return gameState;
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinGame([FromHeader] Guid playerId)
        {
            if (!await _userService.ValidatePlayerId(playerId))
                return Unauthorized();

            var gameId = await _gameService.GetOrCreateGame(playerId);

            if (gameId == null)
                return Unauthorized();

            return Ok(gameId);
        }

        [HttpPost("move/{id}")]
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

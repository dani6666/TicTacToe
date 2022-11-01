using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Core.Interfaces.Services;
using TicTacToe.Core.Model;

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _playersService;

        public PlayersController(IPlayersService playersService)
        {
            _playersService = playersService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<PlayerResult>> Index() => await _playersService.ListPlayerResults();

        //[HttpPost("create")]
        //public ActionResult Create(CreateTableRequest request)
        //{
        //    var operationError = _playersService.Create(request);

        //    return operationError switch
        //    {
        //        MoveResult.NoError => NoContent(),
        //        MoveResult.TableAlreadyExists => BadRequest(),
        //        _ => throw new ApplicationException("Unexpected error occured")
        //    };
        //}

        //[HttpPost("join/{id}")]
        //public ActionResult<JoinTableResponse> Join(int id)
        //{
        //    var response = _playersService.Join(id);

        //    return response.Error switch
        //    {
        //        MoveResult.NoError => response.Data,
        //        MoveResult.TableDoesNotExist => NotFound(),
        //        MoveResult.NoSeatLeftAtTable => BadRequest(),
        //        _ => throw new ApplicationException("Unexpected error occured")
        //    };
        //}

        //[HttpPost("leave/{id}")]
        //public ActionResult Leave(int id, [FromHeader] Guid playerId)
        //{
        //    var operationError = _playersService.Leave(id, playerId);

        //    return operationError switch
        //    {
        //        MoveResult.NoError => NoContent(),
        //        MoveResult.TableDoesNotExist => NotFound(),
        //        MoveResult.PlayerDoesNotExist => Unauthorized(),
        //        _ => throw new ApplicationException("Unexpected error occured")
        //    };
        //}
    }
}

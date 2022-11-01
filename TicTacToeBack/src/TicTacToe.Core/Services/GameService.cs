using System;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Interfaces.Repositories;
using TicTacToe.Core.Interfaces.Services;
using TicTacToe.Core.Model;
using TicTacToe.Core.Model.Enums;
using Player = TicTacToe.Core.Model.Enums.Player;

namespace TicTacToe.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGamesRepository _gamesRepository;

        public GameService(IUserRepository userRepository, IGamesRepository gamesRepository)
        {
            _userRepository = userRepository;
            _gamesRepository = gamesRepository;
        }

        public GameStateResponse GetGame(int gameId, Guid playerId)
        {
            var gameState = _gamesRepository.GetGame(gameId);

            if (gameState == null || (playerId != gameState.CirclePlayerId && playerId != gameState.CrossPlayerId))
                return null;

            bool? isGameWon = gameState.IsEndOfGame
                ? gameState.Winner != null ?
                    gameState.Winner == playerId :
                    null
                : null;

            return new GameStateResponse(gameState.CirclePlayerName, gameState.CrossPlayerName, gameState.IsWaitingForPlayers, gameState.IsEndOfGame,
                isGameWon, gameState.PlayersTurn == playerId, gameState.Board);
        }

        public async Task<int?> GetOrCreateGame(Guid playerId)
        {
            var playerName = await _userRepository.GetPlayerName(playerId);

            if (playerName == null)
                return null;

            var gameId = _gamesRepository.GetAwaitingGameId();

            if (gameId.HasValue)
            {
                _gamesRepository.StartGame(gameId.Value, playerId, playerName);

                return gameId.Value;
            }

            return _gamesRepository.CreateWaitingGame(playerId, playerName);
        }

        public MoveResult MakeMove(int id, Guid playerId, int moveX, int moveY)
        {
            var game = _gamesRepository.GetGame(id);

            if (game == null)
                return MoveResult.NotFoundError;

            if (playerId != game.PlayersTurn)
                return MoveResult.InvalidMove;

            if (playerId != game.CrossPlayerId && playerId != game.CirclePlayerId)
                return MoveResult.NotFoundError;

            if (game.Board[moveX][moveY] != TileType.Empty)
                return MoveResult.InvalidMove;

            game.Board[moveX][moveY] = game.PlayersTurn == game.CirclePlayerId ? TileType.Circle : TileType.Cross;

            var winningPlayer = CheckForWinner(game.Board);

            if (winningPlayer != null)
            {
                if (winningPlayer == Player.Circle)
                {
                    game.Winner = game.CirclePlayerId;
                    _userRepository.UpdateWinnerResults(game.CirclePlayerId);
                    _userRepository.UpdateLoserResults(game.CrossPlayerId);
                }
                else
                {
                    game.Winner = game.CrossPlayerId;
                    _userRepository.UpdateWinnerResults(game.CrossPlayerId);
                    _userRepository.UpdateLoserResults(game.CirclePlayerId);
                }

                game.IsEndOfGame = true;
                return MoveResult.CorrectMove;
            }

            if (game.Board.All(tileTypes => tileTypes.All(tileType => tileType != TileType.Empty)))
            {
                _userRepository.UpdateDrawResults(game.CirclePlayerId, game.CrossPlayerId);

                game.IsEndOfGame = true;
                return MoveResult.CorrectMove;
            }

            game.PlayersTurn = game.PlayersTurn == game.CirclePlayerId ? game.CrossPlayerId : game.CirclePlayerId;

            return MoveResult.CorrectMove;
        }

        private static Player? CheckForWinner(TileType[][] gameBoard)
        {
            foreach (var tileType in new[] { TileType.Circle, TileType.Cross })
            {
                for (var i = 0; i < 3; i++)
                {
                    int j;
                    for (j = 0; j < 3; j++)
                    {
                        if (gameBoard[i][j] != tileType)
                            break;
                    }

                    if (j == 3)
                        return GetWinner(tileType);

                    for (j = 0; j < 3; j++)
                    {
                        if (gameBoard[j][i] != tileType)
                            break;
                    }

                    if (j == 3)
                        return GetWinner(tileType);
                }

                int k;
                for (k = 0; k < 3; k++)
                {
                    if (gameBoard[k][k] != tileType)
                        break;
                }

                if (k == 3)
                    return GetWinner(tileType);

                for (k = 0; k < 3; k++)
                {
                    if (gameBoard[k][2 - k] != tileType)
                        break;
                }

                if (k == 3)
                    return GetWinner(tileType);
            }

            return null;
        }

        private static Player GetWinner(TileType tileType) =>
            tileType == TileType.Circle ? Player.Circle : Player.Cross;
    }
}

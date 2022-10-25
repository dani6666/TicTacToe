using System;
using System.Linq;
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

        public GameState GetGame(int gameId, Guid playerId)
        {
            var gameState = _gamesRepository.GetGame(gameId);

            if (gameState == null || (playerId != gameState.CirclePlayerId && playerId != gameState.CrossPlayerId))
                return null;

            return gameState;
        }

        public int GetOrCreateGame(Guid firstPlayerId)
        {
            var gameId = _gamesRepository.GetAwaitingGameId();

            if (gameId.HasValue)
            {
                _gamesRepository.AddGame(gameId.Value, new GameState(gameId.Value));

                return gameId.Value;
            }

            return _gamesRepository.CreateWaitingGame(firstPlayerId);
        }

        public MoveResult MakeMove(int id, Guid playerId, int moveX, int moveY)
        {
            var game = _gamesRepository.GetGame(id);

            if (game == null)
                return MoveResult.NotFoundError;

            if (playerId == game.CirclePlayerId)
            {
                if (game.PlayersTurn != Player.Circle)
                    return MoveResult.InvalidMove;
            }
            else if (playerId == game.CrossPlayerId)
            {
                if (game.PlayersTurn != Player.Cross)
                    return MoveResult.InvalidMove;
            }
            else
                return MoveResult.NotFoundError;

            if (game.Board[moveX, moveY] != TileType.Empty)
                return MoveResult.InvalidMove;

            game.Board[moveX, moveY] = game.PlayersTurn == Player.Circle ? TileType.Circle : TileType.Cross;

            var winner = CheckForWinner(game.Board);

            if (winner != null)
            {
                if (winner == Player.Circle)
                {
                    _userRepository.UpdateWinnerResults(game.CirclePlayerId);
                    _userRepository.UpdateLoserResults(game.CrossPlayerId);
                }
                else
                {
                    _userRepository.UpdateWinnerResults(game.CrossPlayerId);
                    _userRepository.UpdateLoserResults(game.CirclePlayerId);
                }

                return MoveResult.GameEnded;
            }

            if (game.Board.Cast<TileType>().All(tileType => tileType != TileType.Empty))
            {
                _userRepository.UpdateDrawResults(game.CirclePlayerId, game.CrossPlayerId);

                return MoveResult.GameEnded;
            }

            game.PlayersTurn = game.PlayersTurn == Player.Circle ? Player.Cross : Player.Circle;

            return MoveResult.CorrectMove;
        }

        private static Player? CheckForWinner(TileType[,] gameBoard)
        {
            foreach (var tileType in new[] { TileType.Circle, TileType.Cross })
            {
                for (var i = 0; i < 3; i++)
                {
                    int j;
                    for (j = 0; j < 3; j++)
                    {
                        if (gameBoard[i, j] != tileType)
                            break;
                    }

                    if (j == 3)
                        return GetWinner(tileType);

                    for (j = 0; j < 3; j++)
                    {
                        if (gameBoard[j, i] != tileType)
                            break;
                    }

                    if (j == 3)
                        return GetWinner(tileType);
                }

                int k;
                for (k = 0; k < 3; k++)
                {
                    if (gameBoard[k, k] != tileType)
                        break;
                }

                if (k == 3)
                    return GetWinner(tileType);

                for (k = 0; k < 3; k++)
                {
                    if (gameBoard[k, 2 - k] != tileType)
                        break;
                }

                if (k == 3)
                    return GetWinner(tileType);
            }

            return null;
        }

        private static Player GetWinner(TileType tileType) =>
            tileType == TileType.Circle ? Player.Circle : Player.Cross;

        public void CreateNewGame(int id)
        {
            _gamesRepository.AddGame(id, new GameState(id));
        }

        public void RestartGames()
        {
            var games = _gamesRepository.GetGamesToRestart(DateTime.UtcNow.AddSeconds(-5));

            foreach (var game in games)
                game.Restart();
        }
    }
}

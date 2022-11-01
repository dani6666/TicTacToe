using TicTacToe.Core.Model.Enums;

namespace TicTacToe.Core.Model
{
    public class GameStateResponse
    {
        public string CirclePlayerName{ get; set; }
        public string CrossPlayerName { get; set; }
        public bool IsWaitingForPlayers { get; set; }
        public bool IsEndOfGame { get; set; }
        public bool? IsWon { get; set; }
        public bool IsMyTurn { get; set; }
        public TileType[][] Board { get; set; }

        public GameStateResponse(string circlePlayerName, string crossPlayerName, bool isWaitingForPlayers, bool isEndOfGame, bool? isWon, bool isMyTurn, TileType[][] board)
        {
            CirclePlayerName = circlePlayerName;
            CrossPlayerName = crossPlayerName;
            IsWaitingForPlayers = isWaitingForPlayers;
            IsEndOfGame = isEndOfGame;
            IsWon = isWon;
            IsMyTurn = isMyTurn;
            Board = board;
        }
    }
}

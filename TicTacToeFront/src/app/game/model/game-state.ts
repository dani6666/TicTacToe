export interface GameState {
  circlePlayerName: string
  crossPlayerName: string
  isWaitingForPlayers: boolean
  isEndOfGame: boolean
  isWon: boolean | null
  isMyTurn: boolean
  board: number[][]
}
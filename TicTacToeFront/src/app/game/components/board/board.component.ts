import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { map, Subscription, timer } from 'rxjs';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit {

  private gameId: number = 0

  public board: string[][] = [["", "", ""], ["", "", ""], ["", "", ""]]
  public titleText: string = ""
  public exitButtonsDisabled: boolean = true
  public boardButtonsDisabled: boolean = true

  private timerSubscription: Subscription | null = null

  constructor(private readonly gameService: GameService, 
    private readonly dialog: MatDialog,
    private readonly router: Router) { }

  ngOnInit(): void {
    this.gameService.joinGame().subscribe({
      next: data => {
        this.gameId = data

        this.timerSubscription = timer(0, 1000).pipe( 
          map(() => { 
            this.refreshGameState()
          }) 
        ).subscribe(); 
      },
      error: err => {
        this.handleError()
      }
    });
  }

  ngOnDestroy(): void { 
    this.timerSubscription?.unsubscribe(); 
  } 

  boardClicked(x: number, y: number): void {
    this.gameService.makeMove(this.gameId, x, y).subscribe({
      next: data => {
        this.refreshGameState()
      },
      error: err => {
        this.handleError()
      }
    });
  }

  refreshGameState(): void {
    this.gameService.getGameState(this.gameId).subscribe({
      next: data => {
        if(data.isMyTurn)
          this.boardButtonsDisabled = false
        else
          this.boardButtonsDisabled = true

        if(data.isEndOfGame){
          if(data.isWon == null)
            this.titleText = "Draw"
          else
            this.titleText = data.isWon ? "You won" : "You lost"
            
          this.exitButtonsDisabled = false
          this.boardButtonsDisabled = true
        }
        else if(data.isWaitingForPlayers){
          this.titleText = "Searching for opponent"
          this.boardButtonsDisabled = true
        }
        else
          this.titleText = `${data.circlePlayerName} vs ${data.crossPlayerName}`

        for (let i = 0; i < 3; i++) {
          for (let j = 0; j < 3; j++) {
            var tileStateId = data.board[i][j];
            if(tileStateId == 1)
              this.board[i][j] = "O"
            else if(tileStateId == 2)
              this.board[i][j] = "X"
          }
        }
      },
      error: err => {
        this.handleError()
      }
    });
  }

  handleError(): void{
    let dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { displayLines: ['There was an unexpected error'] }
    });

    dialogRef.afterClosed().subscribe(r => {
      this.router.navigate(["/menu"])
    });
  }

  startNewGame(){

    this.gameId = 0

    this.board =  [["", "", ""], ["", "", ""], ["", "", ""]]
    this.titleText = ""
    this.exitButtonsDisabled = true

    this.timerSubscription = null

    this.ngOnInit()
  }

  exitToMainMenu(){
    this.router.navigate(["/menu"])
  }
}

import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { PlayerRank } from '../../model/player-rank';
import { RankingService } from '../../services/ranking.service';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.scss']
})
export class RankingComponent implements OnInit {

  ranking: PlayerRank[] = [];

  constructor(private readonly rankingService: RankingService, private readonly router: Router,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.rankingService.GetRanking().subscribe({
      next: data =>
      {
        this.ranking = data
      },
      error: err =>
      {
        let dialogRef = this.dialog.open(ConfirmDialogComponent, {
          data: { displayLines: ['Error occured'] }
        });

        dialogRef.afterClosed().subscribe(r => {
          this.router.navigate(["/menu"])
        });
      }
    })
  }

  exitToMainMenu(): void {
    this.router.navigate(["/menu"])
  }

}

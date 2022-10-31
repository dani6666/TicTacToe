import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  constructor(private readonly router: Router) { }

  ngOnInit(): void {
  }

  goToGamePage(): void {
    this.router.navigate(["/game"])
  }

  goToRankingPage(): void {
    this.router.navigate(["/ranking"])
  }
}

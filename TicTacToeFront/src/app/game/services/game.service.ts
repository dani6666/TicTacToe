import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { GameState } from '../model/game-state';

const baseUrl = 'http://'+window.location.hostname+'/api/game/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http: HttpClient) { }

  joinGame(): Observable<number> {
    return this.http.post<number>(baseUrl + 'join', httpOptions);
  }

  makeMove(gameId: number, x: number, y: number): Observable<void> {
    return this.http.post<void>(baseUrl + 'move/' + gameId, { x, y }, httpOptions);
  }

  getGameState(gameId: number): Observable<GameState> {
    return this.http.get<GameState>(baseUrl + gameId, httpOptions);
  }
}

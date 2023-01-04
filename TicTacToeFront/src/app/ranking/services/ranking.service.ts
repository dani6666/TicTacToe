import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PlayerRank } from '../model/player-rank';

const baseUrl = 'http://'+window.location.hostname+'/api/players';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class RankingService {

  constructor(private http: HttpClient) { }

  GetRanking(): Observable<PlayerRank[]> {
    return this.http.get<PlayerRank[]>(baseUrl, httpOptions);
  }
}

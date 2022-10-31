import { Injectable } from '@angular/core';
import { LoggedUser } from '../model/user';

const USER_KEY = 'LoggedUserId';
const GAME_KEY = 'CurrentGameId';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  constructor() { }
  
  signOut(): void {
    window.sessionStorage.clear();
  }

  public saveUserId(id: string): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, id);
  }

  public getUserId(): string | null {
    return window.sessionStorage.getItem(USER_KEY);
  }
}

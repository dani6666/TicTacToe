import { HTTP_INTERCEPTORS, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication/services/authentication.service';
import { TokenStorageService } from './core/services/token-storage.service';

const TOKEN_HEADER_KEY = 'PlayerId';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor(private tokenService: TokenStorageService, private authenticatioService: AuthenticationService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<Object>> {
    const token = this.tokenService.getUserId();

    if (token != null) {
      req = this.addTokenHeader(req, token);
    }

    return next.handle(req);
  }

  private addTokenHeader(request: HttpRequest<any>, token: string) {
    return request.clone({ headers: request.headers.set(TOKEN_HEADER_KEY, token) });
  }
}
export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true }
];
import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { authInterceptorProviders } from './authentication.interceptor';
import { AuthenticationModule } from './authentication/authentication.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { registerLocaleData } from '@angular/common';
import localePl from '@angular/common/locales/pl';
import { CoreModule } from './core/core.module';
import { RankingModule } from './ranking/ranking.module';
registerLocaleData(localePl, 'pl');

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthenticationModule,
    RankingModule,
    CoreModule,
    BrowserAnimationsModule
  ],
  providers: [authInterceptorProviders,{
    provide: LOCALE_ID,
    useValue: 'pl-PL'
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }

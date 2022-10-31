import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './authentication/components/login/login.component';
import { RoleGuard } from './core/guards/role.guard';
import { BoardComponent } from './game/components/board/board.component';
import { MenuComponent } from './menu/components/menu/menu.component';
import { RankingComponent } from './ranking/components/ranking/ranking.component';
  
const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/login'
  },
  {
    path: 'login', 
    component : LoginComponent
  },
  {
    path: 'menu', 
    component : MenuComponent,
    canActivate: [RoleGuard],
    runGuardsAndResolvers: 'always',
  },
  {
    path: 'game', 
    component : BoardComponent,
    canActivate: [RoleGuard],
    runGuardsAndResolvers: 'always',
  },
  {
    path: 'ranking', 
    component : RankingComponent,
    canActivate: [RoleGuard],
    runGuardsAndResolvers: 'always',
  }
];
  
@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
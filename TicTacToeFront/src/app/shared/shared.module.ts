import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    ConfirmDialogComponent,
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    ConfirmDialogComponent
  ]
})
export class SharedModule { }

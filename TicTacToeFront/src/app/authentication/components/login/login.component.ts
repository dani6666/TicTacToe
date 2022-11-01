import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { LoginForm } from '../../model/login-form';
import { AuthenticationService } from '../../services/authentication.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private authService: AuthenticationService, private tokenStorage: TokenStorageService, 
    private formBuilder: FormBuilder, private readonly router: Router, private dialog: MatDialog) { 
      
    this.loginForm = this.formBuilder.group({
      username: new FormControl("", [Validators.required, Validators.minLength(2)]),
      password: new FormControl("", [Validators.required, Validators.minLength(6), Validators.maxLength(100)]),
    })
  }

  ngOnInit(): void {
    // var user = this.tokenStorage.getId()
    // if (user != null) {
    //   this.redirectLoggedUser(user.role);
    // }
  }

  onSubmit(): void {
    const user: LoginForm = this.loginForm.value;

    this.authService.login(user.username, user.password).subscribe({
      next: data => {
        this.tokenStorage.saveUserId(data);
        // this.redirectLoggedUser(data.role);
        this.router.navigate(["/menu"])
      },
      error: err => {
          let dialogRef = this.dialog.open(ConfirmDialogComponent, {
          data: { displayLines: ['Login failed'] }
        });

        dialogRef.afterClosed().subscribe(r => {
          this.loginForm.reset();
        });
      }
    });
  }
}

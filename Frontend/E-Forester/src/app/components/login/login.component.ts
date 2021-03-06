import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  hide = true;
  loginInvalid = false;
  error = "";
  loading = false;

  loginForm!: FormGroup;

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.loginForm= new FormGroup({
      login: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    });
  }

  login(): void {
    if(this.loginForm.valid) {
      const val = this.loginForm.value;
      this.loading = true;

      this.authService.login(val.login, val.password)
        .subscribe({
            complete : () => {
              this.router.navigate(["/"]);
            },
            error : err => {
              this.loading = false;
              this.loginInvalid = true;
              this.error = err;
            }
          });
    }
  }

  cancel(): void {
    this.router.navigate(["/"]);
  }

}

import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  hide = true;
  loginInvalid = false;
  loading = false;

  loginForm!: FormGroup;

  constructor(private router: Router, private auth: AuthService) { }

  ngOnInit(): void {
    this.loginForm= new FormGroup({
      login: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    });
  }

  login(): void {
    const val = this.loginForm.value;
    this.loading = true;

    this.auth.login(val.login, val.password)
      .subscribe({
          complete : () => {
            this.router.navigate(["/"]);
          },
          error : () => {
            this.loading = false;
            this.loginInvalid = true;
          }
        });
  }

  cancel(): void {
    this.router.navigate(["/"]);
  }

}

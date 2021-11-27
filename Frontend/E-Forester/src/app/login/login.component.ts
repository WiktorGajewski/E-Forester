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

  loginForm!: FormGroup;

  constructor(private router: Router, private auth: AuthService) { }

  ngOnInit(): void {
    this.loginForm= new FormGroup({
      login: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required)
    });
  }

  login(formValues: any): void {
    console.log("Próba logowania");
    console.log("Login: " + formValues.login);
    console.log("Hasło: " + formValues.password);

    var result = this.auth.login(formValues.login, formValues.password);

    if(result == true){
      this.loginInvalid = false;
    } 
    else {
      this.loginInvalid = true;
    }
  }

  cancel(): void {
    this.router.navigate(["/"]);
  }

}

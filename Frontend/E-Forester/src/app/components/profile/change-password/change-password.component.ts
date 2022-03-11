import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;
  error = "";

  hideOldPassword = true;
  hideNewPassword = true;

  validation_messages = {
    "newPassword": [
      { type: "required", message: "Pole jest wymagane" },
      { type: "maxlength", message: "Pole może mieć nie wiecej niż 70 znaków" },
      { type: "minlength", message: "Hało musi składać się z conajmniej 8 znaków" },
      { type: "pattern", message: "Hasło musi składać się z małych i dużych liter, cyfr oraz znaków specjalnych: !@#$%^&*?" }
    ]
  };

  constructor(private authService : AuthService, private dialogRef: MatDialogRef<ChangePasswordComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      oldPassword: new FormControl("",  Validators.required),
      newPassword: new FormControl("", Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*?])[a-zA-Z0-9!@#$%^&*?]+$"),
        Validators.maxLength(70)
      ]))
    });
  }

  submit(): void {
    if(this.Form.valid) {
      const val = this.Form.value;
      this.loading = true;
      this.authService.changePassword(val.oldPassword, val.newPassword)
        .subscribe({
          complete : () => {
            this.loading = false;
            this.dialogRef.close(true);
          },
          error : err => {
            this.loading = false;
            this.errorMessage = true;
            this.error = err;
          }
        });
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Role } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/users/user.service';


@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;

  roles = Role;

  validation_messages = {
      "name": [
        { type: "required", message: "Pole jest wymagane" },
        { type: "maxlength", message: "Pole może mieć nie wiecej niż 100 znaków" },
      ],
      "login": [
        { type: "required", message: "Pole jest wymagane" },
        { type: "maxlength", message: "Pole może mieć nie wiecej niż 100 znaków" },
      ],
      "password": [
        { type: "required", message: "Pole jest wymagane" },
        { type: "maxlength", message: "Pole może mieć nie wiecej niż 70 znaków" },
        { type: "minlength", message: "Hało musi składać się z conajmniej 8 znaków" },
        { type: "pattern", message: "Hasło musi składać się conajmniej jednej małej i dużej litery oraz cyfry" }
      ],
      "role": [
        { type: "required", message: "Pole jest wymagane" },
      ]
    }

  constructor(private userService : UserService, private dialogRef: MatDialogRef<CreateUserComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      name: new FormControl("", Validators.compose([
        Validators.required,
        Validators.maxLength(100)
      ])),
      login: new FormControl("",  Validators.compose([
        Validators.required,
        Validators.maxLength(100)
      ])),
      password: new FormControl("", Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]+$"),
        Validators.maxLength(70)
      ])),
      role: new FormControl(null, Validators.required)
    });
  }

  submit(): void {
    if(this.Form.valid) {
      const val = this.Form.value;
      this.loading = true;
      this.userService.registerUser(val.name, val.login, val.password, val.role)
        .subscribe({
          complete : () => {
            this.loading = false;
            this.dialogRef.close(true);
          },
          error : () => {
            this.loading = false;
            this.errorMessage = true;
          }
        });
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }
}

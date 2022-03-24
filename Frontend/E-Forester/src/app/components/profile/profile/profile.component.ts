import { _isNumberValue } from '@angular/cdk/coercion';
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { IUser, Role } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { ChangePasswordComponent } from '../change-password/change-password.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: IUser|null = null;
  roles = Role;

  constructor(private authService: AuthService, private dialog : MatDialog) { }

  ngOnInit(): void {
    this.authService.getProfileInfo()
      .subscribe({
        next: (value: IUser) => {
          this.user = value;
        },
        error: err => {
          console.error(err);
        }
      });
  }

  changePassword(): void {
      const dialogConfig = new MatDialogConfig();

      dialogConfig.disableClose = true;
      dialogConfig.autoFocus = true;

      this.dialog.open(ChangePasswordComponent, dialogConfig);
  }
}

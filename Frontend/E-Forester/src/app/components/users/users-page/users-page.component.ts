import { Component, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateUserComponent } from '../create-user/create-user.component';
import { UsersTableComponent } from '../users-table/users-table.component';

@Component({
  selector: 'app-users-page',
  templateUrl: './users-page.component.html',
  styleUrls: ['./users-page.component.css']
})
export class UsersPageComponent {

  @ViewChild(UsersTableComponent) usersTable !: UsersTableComponent;

  constructor(private dialog : MatDialog) { }

  reloadTable() : void {
    this.usersTable.reloadTable();
  }

  createUserDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreateUserComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      (result: Boolean) =>  {
        if(result == true) {
          this.reloadTable(); 
        }
      }
    ); 
  }
}

import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { Role } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/users/user.service';
import { UsersDataSource } from 'src/app/services/users/users.data-source';
import { CreateUserComponent } from '../create-user/create-user.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit, AfterViewInit {
  dataSource !: UsersDataSource;
  displayedColumns = ["name", "registrationDate", "role", "isActive"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  roles = Role;

  constructor(private userService: UserService, private dialog : MatDialog) { 
    
  }

  ngOnInit(): void {
    this.dataSource = new UsersDataSource(this.userService);
    this.dataSource.loadUsers();
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .subscribe(() => this.loadPage());
  }

  loadPage() : void {
    this.dataSource.loadUsers(
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }

  createUserDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreateUserComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.loadPage(); 
        }
      }
    ); 
  }
}

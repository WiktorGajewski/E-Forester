import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { IUser, Role } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/users/user.service';
import { UsersDataSource } from 'src/app/services/users/users.data-source';
import { AssignForestUnitComponent } from '../assign-forest-unit/assign-forest-unit.component';
import { CreateUserComponent } from '../create-user/create-user.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
  animations: [
    trigger('expandRow', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class UserListComponent implements OnInit, AfterViewInit {
  dataSource !: UsersDataSource;
  displayedColumns = ["name", "registrationDate", "role", "isActive"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  expandedElement: IUser | null = null;

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

  assignForestUnitDialog() : void {
    if(this.expandedElement != null) {
      const dialogConfig = new MatDialogConfig();

      dialogConfig.disableClose = true;
      dialogConfig.autoFocus = true;

      const dialogRef = this.dialog.open(AssignForestUnitComponent, dialogConfig);
      dialogRef.componentInstance.user = this.expandedElement;

      dialogRef.afterClosed().subscribe(
        result =>  {
          if(result == true) {
            this.loadPage(); 
          }
        }
      ); 
    }
  }

  expandElement(row: IUser) : void {
    this.expandedElement = this.expandedElement === row ? null : row;
  }
}

import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IUser, Role } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/users/user.service';
import { UsersDataSource } from 'src/app/services/users/users.data-source';
import { AssignForestUnitComponent } from '../assign-forest-unit/assign-forest-unit.component';
import { ConfirmationComponent } from '../confirmation/confirmation.component';
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
  displayedColumns = ["name", "registrationDate", "role", "isActive", "actions"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  expandedElement: IUser | null = null;

  roles = Role;

  constructor(private userService: UserService, private dialog : MatDialog, private _snackBar: MatSnackBar) { 
    
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

  openSnackBarSuccess(message: string) : void {
    this._snackBar.open(message, "" , {
      duration: 3000,
      panelClass: ['mat-toolbar', 'mat-primary'],
      horizontalPosition: "center",
      verticalPosition: "bottom"
    });
  }

  openSnackBarError(message: string) : void {
    this._snackBar.open(message, "" , {
      duration: 3000,
      panelClass: ['mat-toolbar', 'mat-warn'],
      horizontalPosition: "center",
      verticalPosition: "bottom"
    });
  }

  deleteUser(userId: number, userName: string, event: Event) : void {
    event.stopPropagation();

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = false;

    const dialogRef = this.dialog.open(ConfirmationComponent, dialogConfig);
    dialogRef.componentInstance.message = `Czy na pewno chcesz usunąć użytkownika ${userName}? (zostanie on oznaczony jako nieaktywny a konto zablokowane)`;

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.userService.deactivateUser(userId)
            .subscribe({
              complete : () => {
                this.loadPage();
                this.openSnackBarSuccess("Użytkownik pomyślnie usunięty (zablokowany)");
              },
              error : err => {
                console.error(err);
                this.openSnackBarError("Wystąpił błąd podczas próby usunięcia (zablokowania) użytkownika");
              }
            });
        }
      }
    ); 
  }

  restoreUser(userId: number, userName: string, event: Event) : void {
    event.stopPropagation();

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = false;

    const dialogRef = this.dialog.open(ConfirmationComponent, dialogConfig);
    dialogRef.componentInstance.message = `Czy na pewno chcesz przywrócić użytkownika ${userName}? (zostanie on oznaczony jako aktywny a konto odblokowane)`;

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.userService.reactivateUser(userId)
            .subscribe({
              complete : () => {
                this.loadPage();
                this.openSnackBarSuccess("Pomyślnie przywrócono użytkownika");
              },
              error : err => {
                console.error(err);
                this.openSnackBarError("Wystąpił błąd podczas próby przywrócenia użytkownika");
              }
            });
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

  unassignForestUnit(forestUnitId: number, forestUnitName: string, userId: number, userName: string) : void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = false;

    const dialogRef = this.dialog.open(ConfirmationComponent, dialogConfig);
    dialogRef.componentInstance.message = `Czy na pewno chcesz anulować przypisanie do leśnictwa ${forestUnitName} użytkownikowi ${userName}?`;

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.userService.unassignForestUnit(userId, forestUnitId)
            .subscribe({
              complete : () => {
                this.loadPage();
                this.openSnackBarSuccess("Przypisania do leśnictwa zostało anulowane");
              },
              error : err => {
                console.error(err);
                this.openSnackBarError("Wystąpił błąd podczas próby anulowania przypisania");
              }
            });
        }
      }
    ); 
  }

  expandElement(row: IUser) : void {
    this.expandedElement = this.expandedElement === row ? null : row;
  }
}

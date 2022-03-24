import { Component, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Role } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CreateDivisionComponent } from '../create-division/create-division.component';
import { DivisionsTableComponent } from '../divisions-table/divisions-table.component';

@Component({
  selector: 'app-divisions-page',
  templateUrl: './divisions-page.component.html',
  styleUrls: ['./divisions-page.component.css']
})
export class DivisionsPageComponent {

  selectedForestUnitId: number | null = null;

  @ViewChild(DivisionsTableComponent) divisionsTable !: DivisionsTableComponent;

  userRole: Role | undefined;

  constructor(private dialog : MatDialog, private authService: AuthService) {

    this.authService.authentication.subscribe(auth => 
      {
        this.userRole = auth?.userRole;
      });
   }

  reloadTable() : void {
    this.divisionsTable.reloadTable();
  }

  createDivisionDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreateDivisionComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      (result: Boolean) =>  {
        if(result == true) {
          this.reloadTable(); 
        }
      }
    ); 
  }
}

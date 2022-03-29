import { Component, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Role } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CreateSubareaComponent } from '../create-subarea/create-subarea.component';
import { SubareasTableComponent } from '../subareas-table/subareas-table.component';

@Component({
  selector: 'app-subareas-page',
  templateUrl: './subareas-page.component.html',
  styleUrls: ['./subareas-page.component.css']
})
export class SubareasPageComponent {

  selectedForestUnitId: number | null = null;

  selectedDivisionId: number | null = null;

  @ViewChild(SubareasTableComponent) subareasTable !: SubareasTableComponent;

  userRole: Role | undefined;

  constructor(private dialog : MatDialog, private authService: AuthService) {

    this.authService.authentication.subscribe(auth => 
      {
        this.userRole = auth?.userRole;
      });
  }

  reloadTable() : void {
    this.subareasTable.reloadTable();
  }

  createSubareaDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreateSubareaComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      (result: Boolean) =>  {
        if(result == true) {
          this.reloadTable(); 
        }
      }
    ); 
  }
}

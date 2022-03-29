import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Role } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CreateForestUnitComponent } from '../create-forest-unit/create-forest-unit.component';
import { ForestUnitsTableComponent } from '../forest-units-table/forest-units-table.component';

@Component({
  selector: 'app-forest-units-page',
  templateUrl: './forest-units-page.component.html',
  styleUrls: ['./forest-units-page.component.css']
})
export class ForestUnitsPageComponent implements OnInit {

  @ViewChild(ForestUnitsTableComponent) forestUnitsTable !: ForestUnitsTableComponent;

  userRole: Role | undefined;

  constructor(private dialog : MatDialog, private authService: AuthService) {

    this.authService.authentication.subscribe(auth => 
      {
        this.userRole = auth?.userRole;
      });
  }

  ngOnInit(): void {
  }

  reloadTable() : void {
    this.forestUnitsTable.reloadTable();
  }

  createForestUnitDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreateForestUnitComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      (result: Boolean) =>  {
        if(result == true) {
          this.reloadTable(); 
        }
      }
    ); 
  }
}

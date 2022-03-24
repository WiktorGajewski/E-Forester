import { Component, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreatePlanComponent } from '../create-plan/create-plan.component';
import { PlansTableComponent } from '../plans-table/plans-table.component';

@Component({
  selector: 'app-plans-page',
  templateUrl: './plans-page.component.html',
  styleUrls: ['./plans-page.component.css']
})
export class PlansPageComponent {

  @ViewChild(PlansTableComponent) plansTable !: PlansTableComponent;

  constructor(private dialog : MatDialog) { }

  reloadTable() : void {
    this.plansTable.reloadTable();
  }

  createPlanDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreatePlanComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      (result: Boolean) =>  {
        if(result == true) {
          this.reloadTable(); 
        }
      }
    ); 
  }
}

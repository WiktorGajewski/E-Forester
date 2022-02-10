import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { CreatePlanItemComponent } from '../create-plan-item/create-plan-item.component';
import { PlanItemsTableComponent } from '../plan-items-table/plan-items-table.component';

@Component({
  selector: 'app-plan-items-page',
  templateUrl: './plan-items-page.component.html',
  styleUrls: ['./plan-items-page.component.css']
})
export class PlanItemsPageComponent implements OnInit {

  selectedForestUnitId: number | null = null;

  selectedDivisionId: number | null = null;

  selectedSubareaId: number | null = null;

  selectedPlanId: number | null = null;

  @ViewChild(PlanItemsTableComponent) planItemsTable !: PlanItemsTableComponent;

  constructor(
    private route: ActivatedRoute,
    private dialog : MatDialog) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.selectedForestUnitId = Number(params["forestUnitId"]) || null;
      this.selectedPlanId = Number(params["planId"]) || null;
    });
  }

  reloadTable() : void {
    this.planItemsTable.reloadTable();
  }

  createPlanItemDialog() : void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreatePlanItemComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.reloadTable(); 
        }
      }
    ); 
  }
}

import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { PlanExecutionService } from 'src/app/services/plan-executions/plan-execution.service';
import { PlanExecutionsDataSource } from 'src/app/services/plan-executions/plan-executions.data-source';
import { CreatePlanExecutionComponent } from '../create-plan-execution/create-plan-execution.component';

@Component({
  selector: 'app-plan-executions-table',
  templateUrl: './plan-executions-table.component.html',
  styleUrls: ['./plan-executions-table.component.css']
})
export class PlanExecutionsTableComponent implements OnInit, AfterViewInit {
  dataSource !: PlanExecutionsDataSource;
  displayedColumns = ["executedHectares", "harvestedCubicMeters", "planItemId"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  private _selectedPlanItemId: number|null = null;

  @Input() set selectedPlanItemId(value: number|null) {

    if(value !== undefined && this.selectedPlanItemId !== value) {
      this._selectedPlanItemId = value;
      if(this.dataSource){
        this.reloadTable();
      }
    }
  }

  get selectedPlanItemId() : number| null {
    return this._selectedPlanItemId;
  }

  constructor(private planExecutionService: PlanExecutionService, private dialog : MatDialog) {

  }

  ngOnInit(): void {
    this.dataSource = new PlanExecutionsDataSource(this.planExecutionService);
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .subscribe(() => this.reloadTable());
  }

  reloadTable() : void {
    this.dataSource.loadPlanExecutions(
      this.selectedPlanItemId,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }

  createPlanExecutionDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreatePlanExecutionComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.reloadTable(); 
        }
      }
    ); 
  }
}
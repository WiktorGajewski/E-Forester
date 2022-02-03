import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { PlanExecutionService } from 'src/app/services/plan-executions/plan-execution.service';
import { PlanExecutionsDataSource } from 'src/app/services/plan-executions/plan-executions.data-source';
import { CreatePlanExecutionComponent } from '../create-plan-execution/create-plan-execution.component';

@Component({
  selector: 'app-plan-execution-list',
  templateUrl: './plan-execution-list.component.html',
  styleUrls: ['./plan-execution-list.component.css']
})
export class PlanExecutionListComponent implements OnInit, AfterViewInit {
  dataSource !: PlanExecutionsDataSource;
  displayedColumns = ["executedHectares", "harvestedCubicMeters", "planItemId"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  constructor(private planExecutionService: PlanExecutionService, private dialog : MatDialog) {

  }

  ngOnInit(): void {
    this.dataSource = new PlanExecutionsDataSource(this.planExecutionService);
    this.dataSource.loadPlanExecutions();
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .subscribe(() => this.loadPage());
  }

  loadPage() : void {
    this.dataSource.loadPlanExecutions(
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
          this.loadPage(); 
        }
      }
    ); 
  }
}
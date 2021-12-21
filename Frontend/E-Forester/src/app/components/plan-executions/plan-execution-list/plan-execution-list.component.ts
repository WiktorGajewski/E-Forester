import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { PlanExecutionService } from 'src/app/services/plan-executions/plan-execution.service';
import { PlanExecutionsDataSource } from 'src/app/services/plan-executions/plan-executions.data-source';

@Component({
  selector: 'app-plan-execution-list',
  templateUrl: './plan-execution-list.component.html',
  styleUrls: ['./plan-execution-list.component.css']
})
export class PlanExecutionListComponent implements OnInit, AfterViewInit {
  dataSource !: PlanExecutionsDataSource;
  displayedColumns = ["id", "quantity", "planItemId"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  constructor(private planExecutionService: PlanExecutionService) {

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
      "",
      this.paginator.pageIndex,
      this.paginator.pageSize
    );
  }
}
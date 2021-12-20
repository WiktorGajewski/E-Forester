import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { PlanService } from 'src/app/services/plans/plan.service';
import { PlansDataSource } from 'src/app/services/plans/plans.data-source';

@Component({
  selector: 'app-plan-list',
  templateUrl: './plan-list.component.html',
  styleUrls: ['./plan-list.component.css']
})
export class PlanListComponent implements OnInit, AfterViewInit {
  dataSource !: PlansDataSource;
  displayedColumns = ["id", "year", "forestUnitId"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  constructor(private planService: PlanService) {

  }

  ngOnInit(): void {
    this.dataSource = new PlansDataSource(this.planService);
    this.dataSource.loadPlans();
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .subscribe(() => this.loadPage());
  }

  loadPage() : void {
    this.dataSource.loadPlans(
      "",
      this.paginator.pageIndex,
      this.paginator.pageSize
    );
  }
}
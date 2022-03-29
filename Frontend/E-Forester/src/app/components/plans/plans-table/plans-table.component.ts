import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { NavigationExtras, Router } from '@angular/router';
import { IPlan } from 'src/app/models/plan.model';
import { AuthService } from 'src/app/services/auth/auth.service';
import { PlanService } from 'src/app/services/plans/plan.service';
import { PlansDataSource } from 'src/app/services/plans/plans.data-source';

@Component({
  selector: 'app-plans-table',
  templateUrl: './plans-table.component.html',
  styleUrls: ['./plans-table.component.css']
})
export class PlansTableComponent implements OnInit, AfterViewInit {
  dataSource !: PlansDataSource;
  displayedColumns = ["year", "forestUnitName", "plannedHectares", "executedHectares", "plannedCubicMeters", "harvestedCubicMeters", "completedPlanItems", "isCompleted"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  constructor(private planService: PlanService, private router: Router,
    private authService: AuthService) {

      this.authService.authentication.subscribe(auth => 
        {
          if(auth?.userRole == 1) {
            this.displayedColumns = ["year", "forestUnitName", "plannedHectares", "executedHectares", "plannedCubicMeters", "harvestedCubicMeters", "completedPlanItems", "isCompleted", "actions"];
          }
        });
  }

  ngOnInit(): void {
    this.dataSource = new PlansDataSource(this.planService);
    this.dataSource.loadPlans();
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .subscribe(() => this.reloadTable());
  }

  reloadTable() : void {
    this.dataSource.loadPlans(
      undefined,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }

  clickedPlan(plan : IPlan) : void {
    const navigationsExtras: NavigationExtras = {
      queryParams: {
        planId : plan.id,
        forestUnitId : plan.forestUnitId
      }
    }

    this.router.navigate(["/plan-items"], navigationsExtras);
  }

  markPlanCompleted(planId: number, event: Event) : void {
    event.stopPropagation();
    
    this.planService.markPlanCompleted(planId)
      .subscribe({
        complete : () => {
          this.reloadTable();
        },
        error : err => {
          console.error(err);
        }
      });
  }

  markPlanIncompleted(planId: number, event: Event) : void {
    event.stopPropagation();

    this.planService.markPlanIncompleted(planId)
      .subscribe({
        complete : () => {
          this.reloadTable();
        },
        error : err => {
          console.error(err);
        }
      });
  }
}
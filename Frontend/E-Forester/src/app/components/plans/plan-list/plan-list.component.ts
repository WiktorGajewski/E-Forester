import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { NavigationExtras, Router } from '@angular/router';
import { IPlan } from 'src/app/models/plan.model';
import { PlanService } from 'src/app/services/plans/plan.service';
import { PlansDataSource } from 'src/app/services/plans/plans.data-source';
import { CreatePlanComponent } from '../create-plan/create-plan.component';

@Component({
  selector: 'app-plan-list',
  templateUrl: './plan-list.component.html',
  styleUrls: ['./plan-list.component.css']
})
export class PlanListComponent implements OnInit, AfterViewInit {
  dataSource !: PlansDataSource;
  displayedColumns = ["year", "forestUnitName", "plannedHectares", "executedHectares", "plannedCubicMeters", "harvestedCubicMeters", "isCompleted", "actions"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  constructor(private planService: PlanService, private dialog : MatDialog, private router: Router) {

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
          this.loadPage();
        },
        error : err => {
          console.error(err);
        }
      });
  }

  markPlanIncompleted(planId: number, event: Event) : void {
    event.stopPropagation();
    console.log("open")

    this.planService.markPlanIncompleted(planId)
      .subscribe({
        complete : () => {
          this.loadPage();
        },
        error : err => {
          console.error(err);
        }
      });
  }

  createPlanDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreatePlanComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.loadPage(); 
        }
      }
    ); 
  }
}
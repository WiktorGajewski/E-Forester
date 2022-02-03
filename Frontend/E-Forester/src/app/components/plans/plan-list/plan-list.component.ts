import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
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
  displayedColumns = ["year", "forestUnitId"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  constructor(private planService: PlanService, private dialog : MatDialog) {

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
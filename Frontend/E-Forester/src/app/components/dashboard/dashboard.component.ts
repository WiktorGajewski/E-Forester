import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { PlanService } from 'src/app/services/plans/plan.service';
import { IPage } from 'src/app/models/page.model';
import { IPlan } from 'src/app/models/plan.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  cardLayout = this.breakpointObserver.observe([Breakpoints.XSmall, Breakpoints.Small, Breakpoints.Medium]).pipe(
    map(({ matches }) => {
      if (matches) {
        return {
          columns: 1,
          chart: { cols: 1, rows: 1 },
        };
      }

      return {
        columns: 2,
        chart: { cols: 1, rows: 1 },
      };
    })
  );
  
  _selectedForestUnitId: number | null = null;

  set selectedForestUnitId(value: number|null) {
    this._selectedForestUnitId = value;
    this.reloadCharts();
  }

  get selectedForestUnitId() : number| null {
    return this._selectedForestUnitId;
  }


  plannedHectares : number[] = [];
  executedHectares : number[] = [];
  plannedCubicMeters : number[] = [];
  harvestedCubicMeters : number[] = [];
  labels : string[] = [];

  constructor(
    private breakpointObserver: BreakpointObserver,
    private planService : PlanService) {}

  reloadCharts(): void {
    this.planService.getPlans(this.selectedForestUnitId, 1, 10)
        .subscribe({
          next: (value: IPage<IPlan>) => {
            value.data.reverse()

            this.plannedHectares = value.data.map(t => t.plannedHectares);
            this.executedHectares = value.data.map(t => t.executedHectares);

            this.plannedCubicMeters = value.data.map(t => t.plannedCubicMeters);
            this.harvestedCubicMeters = value.data.map(t => t.harvestedCubicMeters);

            this.labels = value.data.map(t => t.year.toString());
          }
        });
  }
}

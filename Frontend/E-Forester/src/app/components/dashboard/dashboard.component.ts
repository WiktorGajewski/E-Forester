import { Component } from '@angular/core';
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
          columns: 4,
          chart: { cols: 4, rows: 4 },
          miniCard: { cols: 2, rows: 1}
        };
      }

      return {
        columns: 8,
        chart: { cols: 4, rows: 4 },
        miniCard: { cols: 2, rows: 1}
      };
    })
  );
  
  _selectedForestUnitId: number | null = null;

  set selectedForestUnitId(value: number|null) {
    this._selectedForestUnitId = value;
    this.forestUnitIdChange();
  }

  get selectedForestUnitId() : number| null {
    return this._selectedForestUnitId;
  }

  _selectedPlanId: number | null = null;

  set selectedPlanId(value: number|null) {
    this._selectedPlanId = value;
    this.planIdChange();
  }

  get selectedPlanId() : number| null {
    return this._selectedPlanId;
  }
  

  plannedHectares : number[] = [];
  executedHectares : number[] = [];
  plannedCubicMeters : number[] = [];
  harvestedCubicMeters : number[] = [];
  labels : string[] = [];

  //mini-cards
  title ="Tytuł";
  value = "50%";
  //

  constructor(
    private breakpointObserver: BreakpointObserver,
    private planService : PlanService) {}

    forestUnitIdChange(): void {
    this.planService.getPlans(this.selectedForestUnitId, 1, 10)
        .subscribe({
          next: (value: IPage<IPlan>) => {
            value.data.reverse()

            this.plannedHectares = value.data.map(p => p.plannedHectares);
            this.executedHectares = value.data.map(p => p.executedHectares);

            this.plannedCubicMeters = value.data.map(p => p.plannedCubicMeters);
            this.harvestedCubicMeters = value.data.map(p => p.harvestedCubicMeters);

            this.labels = value.data.map(t => t.year.toString());

            this.selectedPlanId = value.data[0].id;
          }
        });
  }

  planIdChange(): void {

    console.log("planIdChange",this.selectedPlanId)
    //
    this.title ="Plan %TEST%";
    this.value ="63.3333%";
    //
  }
}

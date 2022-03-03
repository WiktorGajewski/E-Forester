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

  selectedPlan : IPlan | null = null;
  areaCompletionPercentage : number = 0;
  massCompletionPercentage : number = 0;

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


  constructor(
    private breakpointObserver: BreakpointObserver,
    private planService : PlanService) {}

  forestUnitIdChange(): void {
    if(this.selectedForestUnitId) {
      this.planService.getPlans(this.selectedForestUnitId, 1, 10)
      .subscribe({
        next: (value: IPage<IPlan>) => {
          this.selectedPlanId = value.data[0].id;

          value.data.reverse()

          this.plannedHectares = value.data.map(p => p.plannedHectares);
          this.executedHectares = value.data.map(p => p.executedHectares);

          this.plannedCubicMeters = value.data.map(p => p.plannedCubicMeters);
          this.harvestedCubicMeters = value.data.map(p => p.harvestedCubicMeters);

          this.labels = value.data.map(t => t.year.toString());
        }
      });
    }
    else {
      this.plannedHectares = [];
      this.executedHectares = [];
      this.plannedCubicMeters = [];
      this.harvestedCubicMeters = [];
      this.labels =  [];
      this.selectedPlanId = null;
    }
  }

  planIdChange(): void {
    this.selectedPlan = null;
    this.areaCompletionPercentage = 0;
    this.massCompletionPercentage = 0;

    if(this.selectedPlanId) {
      this.planService.getPlan(this.selectedPlanId)
        .subscribe({
          next: (value: IPlan|undefined) => {
            if(value) {
              this.selectedPlan = value;

              if(value.plannedHectares != 0) {
                this.areaCompletionPercentage = value.executedHectares / value.plannedHectares;
              }

              if(value.plannedCubicMeters != 0) {
                this.massCompletionPercentage = value.harvestedCubicMeters / value.plannedCubicMeters;
              }
            }
            else {
              this.selectedPlanId = null;
            }
          }
        });
    }
  }
}

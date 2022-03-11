import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { PlanService } from 'src/app/services/plans/plan.service';
import { IPage } from 'src/app/models/page.model';
import { IPlan } from 'src/app/models/plan.model';
import moment from 'moment';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
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
  
  selectedForestUnitId: number|null = null;
  selectedPlanId: number|null = null;

  fromYear: number|null = null;
  toYear: number|null = null;

  selectedPlan : IPlan | null = null;
  areaCompletionPercentage : number = 0;
  massCompletionPercentage : number = 0;

  plannedHectares : number[] = [];
  executedHectares : number[] = [];
  plannedCubicMeters : number[] = [];
  harvestedCubicMeters : number[] = [];
  labels : string[] = [];

  constructor(
    private breakpointObserver: BreakpointObserver,
    private planService : PlanService) {}

  ngOnInit(): void {
    this.fromYear = moment().subtract(10,"years").year();
    this.toYear = moment().year();
  }

  forestUnitIdChange(selectedForestUnitId: number|null): void {
    this.selectedForestUnitId = selectedForestUnitId

    if(this.selectedForestUnitId) {
      this.planService.getPlans(this.selectedForestUnitId, null, null, this.fromYear, this.toYear)
      .subscribe({
        next: (value: IPage<IPlan>) => {
          this.planIdChange(value.data[0].id);

          value.data.reverse()

          this.plannedHectares = value.data.map(p => p.plannedHectares);
          this.executedHectares = value.data.map(p => p.executedHectares);

          this.plannedCubicMeters = value.data.map(p => p.plannedCubicMeters);
          this.harvestedCubicMeters = value.data.map(p => p.harvestedCubicMeters);

          this.labels = value.data.map(t => t.year.toString());
        }
      });
    }
  }

  planIdChange(selectedPlanId : number|null): void {
    this.selectedPlanId = selectedPlanId;
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
          }
        });
    }
  }

  dateRangeChange(dateRange: [number, number]): void {
    this.fromYear = dateRange[0];
    this.toYear = dateRange[1];

    if(this.selectedForestUnitId) {
      this.planService.getPlans(this.selectedForestUnitId, null, null, this.fromYear, this.toYear)
      .subscribe({
        next: (value: IPage<IPlan>) => {
          value.data.reverse()

          this.plannedHectares = value.data.map(p => p.plannedHectares);
          this.executedHectares = value.data.map(p => p.executedHectares);

          this.plannedCubicMeters = value.data.map(p => p.plannedCubicMeters);
          this.harvestedCubicMeters = value.data.map(p => p.harvestedCubicMeters);

          this.labels = value.data.map(t => t.year.toString());
        }
      });
    }
  }
}

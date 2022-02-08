import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IPage } from 'src/app/models/page.model';
import { IPlan } from 'src/app/models/plan.model';
import { PlanService } from 'src/app/services/plans/plan.service';

@Component({
  selector: 'app-plan-filter',
  templateUrl: './plan-filter.component.html',
  styleUrls: ['./plan-filter.component.css']
})
export class PlanFilterComponent implements OnInit {

  plans: IPlan[] = [];


  @Input() selectedPlanId: number|undefined = undefined;
  @Output() selectedPlanIdChange = new EventEmitter<number|undefined>();

  constructor(private planService : PlanService) { }

  ngOnInit(): void {

  }

  load(forestUnitId: number|undefined ) : void {
    this.planService.getPlans(forestUnitId, undefined, undefined)
      .subscribe({
          next: (value: IPage<IPlan>) => {
              this.plans = value.data;
          }
      });
  }

  filter() : void {
    this.selectedPlanIdChange.emit(this.selectedPlanId);
  }
}

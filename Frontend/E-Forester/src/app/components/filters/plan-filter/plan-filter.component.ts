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


  @Input() selectedPlanId: number|null = null;
  @Output() selectedPlanIdChange = new EventEmitter<number|null>();

  constructor(private planService : PlanService) { }

  ngOnInit(): void {

  }

  load(forestUnitId: number|null ) : void {
    this.planService.getPlans(forestUnitId, null, null)
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

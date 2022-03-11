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

  private _forestUnitId: number|null = null;

  @Input() set forestUnitId(value: number|null) {
    
    if(this._forestUnitId != null) {
      this.selectedPlanId = null;
      this.planIdChange();
    }

    this._forestUnitId = value;
    
    if(value) {
      this.load();
    }
    else {
      this.plans = [];
    }
  }

  get forestUnitId() : number| null {
    return this._forestUnitId;
  }

  constructor(private planService : PlanService) { }

  ngOnInit(): void {

  }

  load() : void {
    this.planService.getPlans(this.forestUnitId, null, null, null, null)
      .subscribe({
          next: (value: IPage<IPlan>) => {
              this.plans = value.data;
          }
      });
  }

  planIdChange() : void {
    this.selectedPlanIdChange.emit(this.selectedPlanId);
  }
}

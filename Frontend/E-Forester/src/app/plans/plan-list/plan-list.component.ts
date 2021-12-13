import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IPlan } from 'src/app/models/plan.model';
import { PlanService } from 'src/app/services/plan/plan.service';

@Component({
  selector: 'app-plan-list',
  templateUrl: './plan-list.component.html',
  styleUrls: ['./plan-list.component.css']
})
export class PlanListComponent implements OnInit {
  plans: IPlan[] | null = null;

  constructor(private planService: PlanService) {

  }

  ngOnInit(): void {
    this.planService.getPlans()
      .subscribe(result => {
        this.plans = result;
      });
  }
}
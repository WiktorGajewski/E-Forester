import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ActionGroup, IPlanItem } from 'src/app/models/plan-item.model';
import { PlanExecutionService } from 'src/app/services/plan-executions/plan-execution.service';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';

@Component({
  selector: 'app-create-plan-execution',
  templateUrl: './create-plan-execution.component.html',
  styleUrls: ['./create-plan-execution.component.css']
})
export class CreatePlanExecutionComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;
  error = "";

  @Input() selectedPlanItemId: number|null = null;
  selectedPlanItem: IPlanItem|null = null;

  actionGroups = ActionGroup;

  constructor(private planExecutionService : PlanExecutionService,
    private planItemService : PlanItemService,
    private dialogRef: MatDialogRef<CreatePlanExecutionComponent>) { }
  

  ngOnInit(): void {
    this.Form= new FormGroup({
      executedHectares: new FormControl(null, Validators.min(0)),
      harvestedCubicMeters: new FormControl(null, Validators.min(0)),
    });
    
    if(this.selectedPlanItemId) {
      this.planItemService.getPlanItem(this.selectedPlanItemId)
            .subscribe({
              next: (value: IPlanItem) => {
                this.selectedPlanItem = value;
              }
            });
    }
  }

  submit(): void {
    if(this.Form.valid && this.selectedPlanItem) {
      const val = this.Form.value;
      this.loading = true;
      this.planExecutionService.createPlanExecution(val.executedHectares, val.harvestedCubicMeters, this.selectedPlanItem.id, this.selectedPlanItem.planId)
        .subscribe({
          complete : () => {
            this.loading = false;
            this.dialogRef.close(true);
          },
          error : err => {
            this.loading = false;
            this.errorMessage = true;
            this.error = err;
          }
        });
    }
  }

  cancel(): void {
    this.dialogRef.close();
  }
}

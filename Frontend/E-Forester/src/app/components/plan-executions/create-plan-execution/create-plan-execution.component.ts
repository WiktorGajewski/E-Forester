import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PlanExecutionService } from 'src/app/services/plan-executions/plan-execution.service';

@Component({
  selector: 'app-create-plan-execution',
  templateUrl: './create-plan-execution.component.html',
  styleUrls: ['./create-plan-execution.component.css']
})
export class CreatePlanExecutionComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;

  constructor(private planExecutionService : PlanExecutionService, private dialogRef: MatDialogRef<CreatePlanExecutionComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      quantity: new FormControl(null, Validators.required),
      planItemId: new FormControl(null, Validators.required),
      planId: new FormControl(null, Validators.required)
    });
  }

  submit(): void {
    const val = this.Form.value;
    this.loading = true;
    this.planExecutionService.createPlanExecution(val.quantity, val.planItemId, val.planId)
      .subscribe({
        complete : () => {
          this.loading = false;
          this.dialogRef.close(true);
        },
        error : () => {
          this.loading = false;
          this.errorMessage = true;
        }
      });
  }

  cancel(): void {
    this.dialogRef.close();
  }
}

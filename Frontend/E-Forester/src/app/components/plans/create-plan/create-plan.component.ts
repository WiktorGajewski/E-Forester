import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PlanService } from 'src/app/services/plans/plan.service';

@Component({
  selector: 'app-create-plan',
  templateUrl: './create-plan.component.html',
  styleUrls: ['./create-plan.component.css']
})
export class CreatePlanComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;

  constructor(private planService : PlanService, private dialogRef: MatDialogRef<CreatePlanComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      year: new FormControl(null, Validators.required),
      forestUnitId: new FormControl(null, Validators.required)
    });
  }

  submit(): void {
    const val = this.Form.value;
    this.loading = true;
    this.planService.createPlan(val.year, val.forestUnitId)
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

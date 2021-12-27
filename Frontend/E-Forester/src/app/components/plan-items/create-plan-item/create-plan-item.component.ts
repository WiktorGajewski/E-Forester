import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';

@Component({
  selector: 'app-create-plan-item',
  templateUrl: './create-plan-item.component.html',
  styleUrls: ['./create-plan-item.component.css']
})
export class CreatePlanItemComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;

  constructor(private planItemService : PlanItemService, private dialogRef: MatDialogRef<CreatePlanItemComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      quantity: new FormControl(null, Validators.required),
      measureUnit: new FormControl("", Validators.required),
      assortments: new FormControl(null, Validators.required),
      actionGroup: new FormControl(null, Validators.required),
      difficultyLevel: new FormControl(null, Validators.required),
      factor: new FormControl(null, Validators.required),
      planId: new FormControl(null, Validators.required),
      subareaId: new FormControl(null, Validators.required)
    });
  }

  submit(): void {
    const val = this.Form.value;
    this.loading = true;
    this.planItemService.createPlanItem(val.quantity, val.measureUnit, val.assortments, val.actionGroup,
      val.difficultyLevel, val.factor, val.planId, val.subareaId)
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

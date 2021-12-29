import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IDivision } from 'src/app/models/division.model';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPlanItem } from 'src/app/models/plan-item.model';
import { IPlan } from 'src/app/models/plan.model';
import { ISubarea } from 'src/app/models/subarea.model';
import { DivisionService } from 'src/app/services/divisions/division.service';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';
import { PlanExecutionService } from 'src/app/services/plan-executions/plan-execution.service';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';
import { PlanService } from 'src/app/services/plans/plan.service';
import { SubareaService } from 'src/app/services/subareas/subarea.service';

@Component({
  selector: 'app-create-plan-execution',
  templateUrl: './create-plan-execution.component.html',
  styleUrls: ['./create-plan-execution.component.css']
})
export class CreatePlanExecutionComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;

  forestUnits: IForestUnit[] = [];
  divisions: IDivision[] = [];
  subareas: ISubarea[] = [];
  plans: IPlan[] = [];
  planItems: IPlanItem[] = [];

  constructor(private planExecutionService : PlanExecutionService,
    private planItemService : PlanItemService,
    private planService: PlanService,
    private subareaService: SubareaService,
    private divisionService: DivisionService,
    private forestUnitService: ForestUnitService,
    private dialogRef: MatDialogRef<CreatePlanExecutionComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      quantity: new FormControl(null, Validators.required),
      planItemId: new FormControl(null, Validators.required),
      planId: new FormControl(null, Validators.required),
      forestUnitId: new FormControl(null, Validators.required),
      divisionId: new FormControl({value: null, disabled: true}, Validators.required),
      subareaId: new FormControl({value: null, disabled: true}, Validators.required)
    });

    this.Form.controls['divisionId'].disable();
    this.Form.controls['subareaId'].disable();
    this.Form.controls['planId'].disable();
    this.Form.controls['planItemId'].disable();

    this.forestUnitService.getForestUnits()
            .subscribe({
                next: (value: IForestUnit[]) => {
                    this.forestUnits = value;
                }
            });
  }

  forestUnitSelected(forestUnitId: number) {
    this.divisionService.getDivisions(forestUnitId)
            .subscribe({
                next: (value: IDivision[]) => {
                    this.divisions = value;
                    this.Form.controls['divisionId'].enable()
                }
            });
    
    this.planService.getPlans(forestUnitId)
    .subscribe({
        next: (value: IPlan[]) => {
            this.plans = value;
            this.Form.controls['planId'].enable()
        }
    });
  }

  divisionSelected(divisionId: number) {
    this.subareaService.getSubareas(divisionId)
            .subscribe({
                next: (value: ISubarea[]) => {
                    this.subareas = value;
                    this.Form.controls['subareaId'].enable()
                }
            });
  }

  subareaSelected(subareaId: number) {
    this.planItemService.getPlanItems(subareaId)
            .subscribe({
                next: (value: IPlanItem[]) => {
                    this.planItems = value;
                    this.Form.controls['planItemId'].enable()
                }
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

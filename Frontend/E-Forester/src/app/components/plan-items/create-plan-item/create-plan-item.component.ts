import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IDivision } from 'src/app/models/division.model';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPage } from 'src/app/models/page.model';
import { ActionGroup, WoodAssortment } from 'src/app/models/plan-item.model';
import { IPlan } from 'src/app/models/plan.model';
import { ISubarea } from 'src/app/models/subarea.model';
import { DivisionService } from 'src/app/services/divisions/division.service';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';
import { PlanService } from 'src/app/services/plans/plan.service';
import { SubareaService } from 'src/app/services/subareas/subarea.service';

@Component({
  selector: 'app-create-plan-item',
  templateUrl: './create-plan-item.component.html',
  styleUrls: ['./create-plan-item.component.css']
})
export class CreatePlanItemComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;

  forestUnits: IForestUnit[] = [];
  divisions: IDivision[] = [];
  subareas: ISubarea[] = [];
  plans: IPlan[] = [];

  actionGroups = ActionGroup;
  assortments = WoodAssortment;

  constructor(private planItemService : PlanItemService,
    private planService: PlanService,
    private subareaService: SubareaService,
    private divisionService: DivisionService,
    private forestUnitService: ForestUnitService,
    private dialogRef: MatDialogRef<CreatePlanItemComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      plannedHectares: new FormControl(null, Validators.required),
      plannedCubicMeters: new FormControl(null, Validators.required),
      assortments: new FormControl(null, Validators.required),
      actionGroup: new FormControl(null, Validators.required),
      difficultyLevel: new FormControl(null, Validators.required),
      factor: new FormControl(null, Validators.required),
      planId: new FormControl({value: null, disabled: true}, Validators.required),
      forestUnitId: new FormControl(null, Validators.required),
      divisionId: new FormControl({value: null, disabled: true}, Validators.required),
      subareaId: new FormControl({value: null, disabled: true}, Validators.required)
    });

    this.Form.controls['divisionId'].disable();
    this.Form.controls['subareaId'].disable();
    this.Form.controls['planId'].disable();


    this.forestUnitService.getForestUnits(null, null)
            .subscribe({
                next: (value: IPage<IForestUnit>) => {
                    this.forestUnits = value.data;
                }
            });
  }
  
  forestUnitSelected(forestUnitId: number) {
    this.divisionService.getDivisions(forestUnitId, null, null)
      .subscribe({
          next: (value: IPage<IDivision>) => {
              this.divisions = value.data;
              this.Form.controls['divisionId'].enable()
          }
      });
    
    this.planService.getPlans(forestUnitId, null, null)
      .subscribe({
          next: (value: IPage<IPlan>) => {
              this.plans = value.data;
              this.Form.controls['planId'].enable()
          }
      });
  }

  divisionSelected(divisionId: number) {
    this.subareaService.getSubareas(null, divisionId, null, null)
            .subscribe({
                next: (value: IPage<ISubarea>) => {
                    this.subareas = value.data;
                    this.Form.controls['subareaId'].enable()
                }
            });
  }

  submit(): void {
    const val = this.Form.value;
    let assortments = 0;

    if(val.assortments) {
      const selectedAssortmentsArray : [] = val.assortments;
      assortments = selectedAssortmentsArray.reduce((a, b) => a + b, 0);
    }

    this.loading = true;
    this.planItemService.createPlanItem(val.plannedHectares, val.plannedCubicMeters, assortments, val.actionGroup,
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

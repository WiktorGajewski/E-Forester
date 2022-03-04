import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPage } from 'src/app/models/page.model';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';
import { PlanService } from 'src/app/services/plans/plan.service';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import * as _moment from 'moment';
import {default as _rollupMoment, Moment} from 'moment';
import { MatDatepicker } from '@angular/material/datepicker';

const moment = _rollupMoment || _moment;

const MY_FORMATS = {
  parse: {
    dateInput: 'YYYY'
  },
  display: {
    dateInput: 'YYYY',
    monthYearLabel: 'YYYY',
    dateA11yLabel: 'YYYY',
    monthYearA11yLabel: 'YYYY',
  }
};

@Component({
  selector: 'app-create-plan',
  templateUrl: './create-plan.component.html',
  styleUrls: ['./create-plan.component.css'],
  providers: [
    {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS}
  ]
})
export class CreatePlanComponent implements OnInit {
  Form!: FormGroup;
  forestUnitId = new FormControl(null, Validators.required);
  year = new FormControl(moment(), Validators.required);

  loading = false;
  errorMessage = false;

  forestUnits: IForestUnit[] = [];

  constructor(private planService : PlanService,
    private forestUnitService: ForestUnitService,
    private dialogRef: MatDialogRef<CreatePlanComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      year: this.year,
      forestUnitId: this.forestUnitId
    });

    this.forestUnitService.getForestUnits(null, null)
            .subscribe({
                next: (value: IPage<IForestUnit>) => {
                    this.forestUnits = value.data;
                }
            });
  }

   yearSelected(normalizedYear: Moment, datepicker: MatDatepicker<Moment>): void {
    const ctrlValue = moment();
    ctrlValue.year(normalizedYear.year());
    this.year.setValue(ctrlValue);
    datepicker.close();
  }

  submit(): void {
    if(this.Form.valid) {
      const val = this.Form.value;

      this.loading = true;
      this.planService.createPlan(val.year.year(), val.forestUnitId)
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
  }

  cancel(): void {
    this.dialogRef.close();
  }
}

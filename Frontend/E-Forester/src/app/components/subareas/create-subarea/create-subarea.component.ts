import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IDivision } from 'src/app/models/division.model';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPage } from 'src/app/models/page.model';
import { DivisionService } from 'src/app/services/divisions/division.service';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';
import { SubareaService } from 'src/app/services/subareas/subarea.service';

@Component({
  selector: 'app-create-subarea',
  templateUrl: './create-subarea.component.html',
  styleUrls: ['./create-subarea.component.css']
})
export class CreateSubareaComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;

  forestUnits: IForestUnit[] = [];
  divisions: IDivision[] = [];

  constructor(private subareaService: SubareaService,
    private divisionService: DivisionService,
    private forestUnitService: ForestUnitService,
    private dialogRef: MatDialogRef<CreateSubareaComponent>) {}

  ngOnInit(): void {
    this.Form= new FormGroup({
      address: new FormControl("", Validators.required),
      area: new FormControl(null, Validators.required),
      forestUnitId: new FormControl(null, Validators.required),
      division: new FormControl({value: null, disabled: true}, Validators.required)
    });

    this.Form.controls['division'].disable();

    this.forestUnitService.getForestUnits(undefined, undefined)
            .subscribe({
                next: (value: IPage<IForestUnit>) => {
                    this.forestUnits = value.data;
                }
            });
  }

  forestUnitSelected(forestUnitId: number) {
    this.divisionService.getDivisions(forestUnitId, undefined, undefined)
            .subscribe({
                next: (value: IPage<IDivision>) => {
                    this.divisions = value.data;
                    this.Form.controls['division'].enable()
                }
            });
  }

  divisionSelected(division: IDivision) {
    if(this.Form.controls['address'].untouched || this.Form.controls['address'].value == "") {
      this.Form.controls['address'].setValue(division.address);
    }
  }

  submit(): void {
    const val = this.Form.value;
    this.loading = true;
    this.subareaService.createSubarea(val.address, val.area, val.division.id)
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
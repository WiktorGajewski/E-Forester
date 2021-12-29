import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IDivision } from 'src/app/models/division.model';
import { IForestUnit } from 'src/app/models/forest-unit.model';
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
    private dialogRef: MatDialogRef<CreateSubareaComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      address: new FormControl("", Validators.required),
      area: new FormControl(null, Validators.required),
      divisionId: new FormControl(null, Validators.required)
    });

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
                }
            });
  }

  submit(): void {
    const val = this.Form.value;
    this.loading = true;
    this.subareaService.createSubarea(val.address, val.area, val.divisionId)
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
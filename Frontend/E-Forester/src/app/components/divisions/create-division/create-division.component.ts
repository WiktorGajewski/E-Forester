import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPage } from 'src/app/models/page.model';
import { DivisionService } from 'src/app/services/divisions/division.service';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';

@Component({
  selector: 'app-create-division',
  templateUrl: './create-division.component.html',
  styleUrls: ['./create-division.component.css']
})
export class CreateDivisionComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;

  forestUnits: IForestUnit[] = [];

  constructor(private divisionService : DivisionService,
      private forestUnitService : ForestUnitService,
      private dialogRef: MatDialogRef<CreateDivisionComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      address: new FormControl("", Validators.required),
      area: new FormControl(null, Validators.required),
      forestUnit: new FormControl(null, Validators.required)
    });

    this.forestUnitService.getForestUnits(undefined, undefined)
            .subscribe({
                next: (value: IPage<IForestUnit>) => {
                    this.forestUnits = value.data;
                }
            });
  }

  forestUnitSelected(forestUnit: IForestUnit) {
    if(this.Form.controls['address'].untouched  || this.Form.controls['address'].value == "") {
      this.Form.controls['address'].setValue(forestUnit.address);
    }
  }

  submit(): void {
    const val = this.Form.value;
    this.loading = true;
    this.divisionService.createDivision(val.address, val.area, val.forestUnit.id)
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
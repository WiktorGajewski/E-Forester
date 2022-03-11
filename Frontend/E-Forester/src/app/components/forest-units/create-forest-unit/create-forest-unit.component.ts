import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';

@Component({
  selector: 'app-create-forest-unit',
  templateUrl: './create-forest-unit.component.html',
  styleUrls: ['./create-forest-unit.component.css']
})
export class CreateForestUnitComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;
  error = "";

  constructor(private forestUnitService : ForestUnitService, private dialogRef: MatDialogRef<CreateForestUnitComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      name: new FormControl("", Validators.required),
      address: new FormControl("", Validators.required),
      area: new FormControl(null, Validators.required)
    });
  }

  submit(): void {
    if(this.Form.valid) {
      const val = this.Form.value;
      this.loading = true;
      this.forestUnitService.createForestUnit(val.name, val.address, val.area)
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
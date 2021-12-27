import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { DivisionService } from 'src/app/services/divisions/division.service';

@Component({
  selector: 'app-create-division',
  templateUrl: './create-division.component.html',
  styleUrls: ['./create-division.component.css']
})
export class CreateDivisionComponent implements OnInit {
  Form!: FormGroup;

  loading = false;
  errorMessage = false;

  constructor(private divisionService : DivisionService, private dialogRef: MatDialogRef<CreateDivisionComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      address: new FormControl("", Validators.required),
      area: new FormControl(null, Validators.required),
      forestUnitId: new FormControl(null, Validators.required)
    });
  }

  submit(): void {
    const val = this.Form.value;
    this.loading = true;
    this.divisionService.createDivision(val.name, val.address, val.forestUnitId)
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
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-forest-unit',
  templateUrl: './create-forest-unit.component.html',
  styleUrls: ['./create-forest-unit.component.css']
})
export class CreateForestUnitComponent implements OnInit {
  Form!: FormGroup;

  loading = false;

  constructor(private router: Router, private dialogRef: MatDialogRef<CreateForestUnitComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      name: new FormControl("", Validators.required),
      address: new FormControl("", Validators.required),
      area: new FormControl(null, Validators.required)
    });
  }

  submit(): void {
    const val = this.Form.value;
    this.dialogRef.close(val);
  }

  cancel(): void {
    this.dialogRef.close();
  }
}
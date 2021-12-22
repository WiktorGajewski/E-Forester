import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-forest-unit',
  templateUrl: './create-forest-unit.component.html',
  styleUrls: ['./create-forest-unit.component.css']
})
export class CreateForestUnitComponent implements OnInit {
  Form!: FormGroup;

  loading = false;

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      name: new FormControl("", Validators.required),
      address: new FormControl("", Validators.required),
      area: new FormControl(null, Validators.required)
    });
  }

  submit(): void {
    const val = this.Form.value;
    console.log(val);
  }

  cancel(): void {
    this.router.navigate(["/"]);
  }
}
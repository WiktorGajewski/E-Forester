import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPage } from 'src/app/models/page.model';
import { IUser } from 'src/app/models/user.model';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';
import { UserService } from 'src/app/services/users/user.service';

@Component({
  selector: 'app-assign-forest-unit',
  templateUrl: './assign-forest-unit.component.html',
  styleUrls: ['./assign-forest-unit.component.css']
})
export class AssignForestUnitComponent implements OnInit {
  Form!: FormGroup;

  @Input() user!: IUser;

  loading = false;
  errorMessage = false;
  error = "";

  forestUnits: IForestUnit[] = [];

  constructor(private forestUnitService : ForestUnitService, private userService : UserService,
      private dialogRef: MatDialogRef<AssignForestUnitComponent>) { }

  ngOnInit(): void {
    this.Form= new FormGroup({
      forestUnit: new FormControl(null, Validators.required)
    });

    this.forestUnitService.getForestUnits(null, null)
            .subscribe({
                next: (value: IPage<IForestUnit>) => {
                    this.forestUnits = value.data.filter(forestUnit => 
                      !this.user.assignedForestUnits.some(e => e.id == forestUnit.id));
                }
            });
  }

  submit(): void {
    if(this.Form.valid) {
      const val = this.Form.value;
      this.loading = true;
      this.userService.assignForestUnit(this.user.id, val.forestUnit.id)
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

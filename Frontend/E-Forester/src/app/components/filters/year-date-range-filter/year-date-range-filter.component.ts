import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { MatDateRangePicker } from '@angular/material/datepicker';


import * as _moment from 'moment';
import {default as _rollupMoment, Moment} from 'moment';

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
  selector: 'year-date-range-filter',
  templateUrl: './year-date-range-filter.component.html',
  styleUrls: ['./year-date-range-filter.component.css'],
  providers: [
    {provide: MAT_DATE_FORMATS, useValue: MY_FORMATS}
  ]
})
export class YearDateRangeFilterComponent {
  @Output() dateRangeChange = new EventEmitter<[number,number]>();

  start = new FormControl(moment().subtract(10,"years"), Validators.required);
  end = new FormControl(moment(), Validators.required);

  range = new FormGroup({
    start: this.start,
    end: this.end,
  });

  mclick = 0;

  constructor() { }

  yearSelected(normalizedYear: Moment, dateRangePicker: MatDateRangePicker<Moment>): void {
    this.mclick++;

    if(this.mclick == 1) {
      const ctrlValue = moment();
      ctrlValue.year(normalizedYear.year());
      this.start.setValue(ctrlValue);
      dateRangePicker.close();
    }
    else {
      const ctrlValue = moment();
      ctrlValue.year(normalizedYear.year());
      this.end.setValue(ctrlValue);
      dateRangePicker.close();

      this.mclick = 0;
    }
  }

  filter(): void {
    if(this.range.valid) {
      this.dateRangeChange.emit([this.start.value.year(),this.end.value.year()]);
    }
  }
}

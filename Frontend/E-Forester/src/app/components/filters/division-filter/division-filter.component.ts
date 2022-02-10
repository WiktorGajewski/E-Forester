import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IDivision } from 'src/app/models/division.model';
import { IPage } from 'src/app/models/page.model';
import { DivisionService } from 'src/app/services/divisions/division.service';

@Component({
  selector: 'app-division-filter',
  templateUrl: './division-filter.component.html',
  styleUrls: ['./division-filter.component.css']
})
export class DivisionFilterComponent implements OnInit {

  divisions: IDivision[] = [];

  @Input() selectedDivisionId: number|null = null;
  @Output() selectedDivisionIdChange = new EventEmitter<number|null>();

  private _forestUnitId: number|null = null;

  @Input() set forestUnitId(value: number|null) {

    if(this._forestUnitId != null) {
      this.selectedDivisionId = null;
      this.divisionIdChange();
    }

    this._forestUnitId = value;

    if(value) {
      this.load();
    }
    else {
      this.divisions = [];
    }
  }

  get forestUnitId() : number| null {
    return this._forestUnitId;
  }

  constructor(private divisionService : DivisionService) { }

  ngOnInit(): void {

  }

  load() : void {
    this.divisionService.getDivisions(this.forestUnitId, null, null)
      .subscribe({
          next: (value: IPage<IDivision>) => {
              this.divisions = value.data;
          }
      });
  }

  divisionIdChange() : void {
    this.selectedDivisionIdChange.emit(this.selectedDivisionId);
  }
}

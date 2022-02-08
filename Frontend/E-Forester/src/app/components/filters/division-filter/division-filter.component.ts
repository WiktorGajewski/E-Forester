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

  @Input() selectedDivisionId: number|undefined = undefined;
  @Output() selectedDivisionIdChange = new EventEmitter<number|undefined>();

  constructor(private divisionService : DivisionService) { }

  ngOnInit(): void {

  }

  load(forestUnitId: number|undefined) : void {
    this.divisionService.getDivisions(forestUnitId, undefined, undefined)
      .subscribe({
          next: (value: IPage<IDivision>) => {
              this.divisions = value.data;
          }
      });
  }

  filter() : void {
    this.selectedDivisionIdChange.emit(this.selectedDivisionId);
  }
}

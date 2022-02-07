import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPage } from 'src/app/models/page.model';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';

@Component({
  selector: 'app-forest-unit-filter',
  templateUrl: './forest-unit-filter.component.html',
  styleUrls: ['./forest-unit-filter.component.css']
})
export class ForestUnitFilterComponent implements OnInit {

  forestUnits: IForestUnit[] = [];
  
  @Input() selectedForestUnitId: number|undefined = undefined;
  @Output() selectedForestUnitIdChange = new EventEmitter<number|undefined>();
  
  constructor(private forestUnitService: ForestUnitService) { }

  ngOnInit(): void {
    this.getForestUnits();
  }

  getForestUnits() : void {
    this.forestUnitService.getForestUnits(undefined, undefined)
      .subscribe({
          next: (value: IPage<IForestUnit>) => {
              this.forestUnits = value.data;

              if(this.selectedForestUnitId) {
                this.filter();
              }
          }
      });
  }

  filter() : void {
    this.selectedForestUnitIdChange.emit(this.selectedForestUnitId);
  }
}

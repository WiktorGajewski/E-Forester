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
  
  @Input() selectedForestUnitId: number|null = null;
  @Output() selectedForestUnitIdChange = new EventEmitter<number|null>();
  
  constructor(private forestUnitService: ForestUnitService) { }

  ngOnInit(): void {

  }

  load() : void {
    this.forestUnitService.getForestUnits(null, null)
      .subscribe({
          next: (value: IPage<IForestUnit>) => {
              this.forestUnits = value.data;
          }
      });
  }

  loadAndFilter() : void {
    this.forestUnitService.getForestUnits(null, null)
      .subscribe({
          next: (value: IPage<IForestUnit>) => {
              this.forestUnits = value.data;

              if(!this.selectedForestUnitId && this.forestUnits.length > 0) {
                this.selectedForestUnitId = this.forestUnits[0]?.id;
              }

              this.filter();
          }
      });
  }

  filter() : void {
    this.selectedForestUnitIdChange.emit(this.selectedForestUnitId);
  }
}

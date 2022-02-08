import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IPage } from 'src/app/models/page.model';
import { ISubarea } from 'src/app/models/subarea.model';
import { SubareaService } from 'src/app/services/subareas/subarea.service';

@Component({
  selector: 'app-subarea-filter',
  templateUrl: './subarea-filter.component.html',
  styleUrls: ['./subarea-filter.component.css']
})
export class SubareaFilterComponent implements OnInit {

  subareas: ISubarea[] = [];

  @Input() selectedSubareaId: number|undefined = undefined;
  @Output() selectedSubareaIdChange = new EventEmitter<number|undefined>();

  constructor(private subareaService : SubareaService) { }

  ngOnInit(): void {

  }

  load(divisionId: number|undefined) : void {
    this.subareaService.getSubareas(divisionId, undefined, undefined)
      .subscribe({
          next: (value: IPage<ISubarea>) => {
              this.subareas = value.data;
          }
      });
  }

  filter() : void {
    this.selectedSubareaIdChange.emit(this.selectedSubareaId);
  }
}

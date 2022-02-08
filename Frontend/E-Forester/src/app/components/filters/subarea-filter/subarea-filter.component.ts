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

  @Input() selectedSubareaId: number|null = null;
  @Output() selectedSubareaIdChange = new EventEmitter<number|null>();

  constructor(private subareaService : SubareaService) { }

  ngOnInit(): void {

  }

  load(divisionId: number|null) : void {
    this.subareaService.getSubareas(null, divisionId, null, null)
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

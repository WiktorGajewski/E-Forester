import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge, tap } from 'rxjs';
import { SubareaDataSource } from '../services/subareas/subarea.data-source';
import { SubareaService } from '../services/subareas/subarea.service';

@Component({
  selector: 'app-subarea-list',
  templateUrl: './subarea-list.component.html',
  styleUrls: ['./subarea-list.component.css']
})
export class SubareaListComponent implements OnInit, AfterViewInit {
  dataSource !: SubareaDataSource;
  displayedColumns = ["id", "address", "area"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;
  @ViewChild(MatSort) sort !: MatSort;

  constructor(private subareaService : SubareaService) { 

  }
  
  ngOnInit(): void {
    this.dataSource = new SubareaDataSource(this.subareaService);
    this.dataSource.loadSubareas();
  }

  ngAfterViewInit(): void {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .subscribe(() => this.loadPage());
  }

  loadPage() : void {
    this.dataSource.loadSubareas(
      "",
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize
    );
  }
}

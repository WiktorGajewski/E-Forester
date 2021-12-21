import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge } from 'rxjs';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';
import { ForestUnitsDataSource } from 'src/app/services/forest-units/forest-units.data-source';


@Component({
  selector: 'app-forest-unit-list',
  templateUrl: './forest-unit-list.component.html',
  styleUrls: ['./forest-unit-list.component.css']
})
export class ForestUnitListComponent implements OnInit, AfterViewInit {
  dataSource !: ForestUnitsDataSource;
  displayedColumns = ["id", "name", "address", "area"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;
  @ViewChild(MatSort) sort !: MatSort;

  constructor(private forestUnitService : ForestUnitService) {

  }

  ngOnInit(): void {
    this.dataSource = new ForestUnitsDataSource(this.forestUnitService);
    this.dataSource.loadForestUnits();
  }

  ngAfterViewInit(): void {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .subscribe(() => this.loadPage());
  }

  loadPage() : void {
    this.dataSource.loadForestUnits(
      "",
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize
    );
  }
}
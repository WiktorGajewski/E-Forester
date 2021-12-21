import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge } from 'rxjs';
import { DivisionService } from 'src/app/services/divisions/division.service';
import { DivisionsDataSource } from 'src/app/services/divisions/divisions.data-source';

@Component({
  selector: 'app-division-list',
  templateUrl: './division-list.component.html',
  styleUrls: ['./division-list.component.css']
})
export class DivisionListComponent implements OnInit, AfterViewInit {
  dataSource !: DivisionsDataSource;
  displayedColumns = ["id", "address", "area"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;
  @ViewChild(MatSort) sort !: MatSort;

  constructor(private divisionService : DivisionService) {

  }

  ngOnInit(): void {
    this.dataSource = new DivisionsDataSource(this.divisionService);
    this.dataSource.loadDivisions();
  }

  ngAfterViewInit(): void {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .subscribe(() => this.loadPage());
  }

  loadPage() : void {
    this.dataSource.loadDivisions(
      "",
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize
    );
  }
}
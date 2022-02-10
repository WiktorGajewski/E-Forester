import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { SubareasDataSource } from 'src/app/services/subareas/subareas.data-source';
import { SubareaService } from 'src/app/services/subareas/subarea.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateSubareaComponent } from '../create-subarea/create-subarea.component';
import { ForestUnitFilterComponent } from '../../filters/forest-unit-filter/forest-unit-filter.component';
import { DivisionFilterComponent } from '../../filters/division-filter/division-filter.component';

@Component({
  selector: 'app-subarea-list',
  templateUrl: './subarea-list.component.html',
  styleUrls: ['./subarea-list.component.css']
})
export class SubareaListComponent implements OnInit, AfterViewInit {
  dataSource !: SubareasDataSource;
  displayedColumns = ["address", "area"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  selectedForestUnitId: number | null = null;

  selectedDivisionId: number | null = null;

  constructor(private subareaService : SubareaService,
    private dialog : MatDialog) { 

  }
  
  ngOnInit(): void {
    this.dataSource = new SubareasDataSource(this.subareaService);
  }

  ngAfterViewInit(): void {

    this.paginator.page
      .subscribe(() => this.loadPage());
  }

  loadPage() : void {
    this.dataSource.loadSubareas(
      this.selectedForestUnitId,
      this.selectedDivisionId,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }

  filter() : void {
    this.paginator.pageIndex = 0;
    this.loadPage();
  }

  createSubareaDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreateSubareaComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.loadPage(); 
        }
      }
    ); 
  }
}

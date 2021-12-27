import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge } from 'rxjs';
import { SubareasDataSource } from 'src/app/services/subareas/subareas.data-source';
import { SubareaService } from 'src/app/services/subareas/subarea.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateSubareaComponent } from '../create-subarea/create-subarea.component';

@Component({
  selector: 'app-subarea-list',
  templateUrl: './subarea-list.component.html',
  styleUrls: ['./subarea-list.component.css']
})
export class SubareaListComponent implements OnInit, AfterViewInit {
  dataSource !: SubareasDataSource;
  displayedColumns = ["id", "address", "area"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;
  @ViewChild(MatSort) sort !: MatSort;

  constructor(private subareaService : SubareaService, private dialog : MatDialog) { 

  }
  
  ngOnInit(): void {
    this.dataSource = new SubareasDataSource(this.subareaService);
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

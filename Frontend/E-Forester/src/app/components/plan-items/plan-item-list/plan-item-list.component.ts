import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { ActionGroup, WoodAssortment } from 'src/app/models/plan-item.model';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';
import { PlanItemsDataSource } from 'src/app/services/plan-items/plan-items.data-source';
import { DivisionFilterComponent } from '../../filters/division-filter/division-filter.component';
import { ForestUnitFilterComponent } from '../../filters/forest-unit-filter/forest-unit-filter.component';
import { PlanFilterComponent } from '../../filters/plan-filter/plan-filter.component';
import { SubareaFilterComponent } from '../../filters/subarea-filter/subarea-filter.component';
import { CreatePlanItemComponent } from '../create-plan-item/create-plan-item.component';

@Component({
  selector: 'app-plan-item-list',
  templateUrl: './plan-item-list.component.html',
  styleUrls: ['./plan-item-list.component.css']
})
export class PlanItemListComponent implements OnInit, AfterViewInit {
  dataSource !: PlanItemsDataSource;
  displayedColumns = ["isCompleted", "plannedHectares", "plannedCubicMeters", "woodAssortment", "actionGroup", "difficultyLevel", "factor"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  @ViewChild(ForestUnitFilterComponent) forestUnitFilter !: ForestUnitFilterComponent;
  selectedForestUnitId: number | null = null;

  @ViewChild(DivisionFilterComponent) divisionFilter !: DivisionFilterComponent;
  selectedDivisionId: number | null = null;

  @ViewChild(SubareaFilterComponent) subareaFilter !: SubareaFilterComponent;
  selectedSubareaId: number | null = null;

  @ViewChild(PlanFilterComponent) planFilter !: PlanFilterComponent;
  selectedPlanId: number | null = null;

  actionGroups = ActionGroup;
    
  constructor(private planItemService: PlanItemService, 
    private route: ActivatedRoute,
    private dialog : MatDialog,
    private cd: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.dataSource = new PlanItemsDataSource(this.planItemService);
  }

  ngAfterViewInit(): void {

    this.route.queryParams.subscribe(params => {
      this.selectedPlanId = Number(params["planId"]) || null;
      this.selectedForestUnitId = Number(params["forestUnitId"]) || null;

      this.filterOnInit();
    });

    this.cd.detectChanges();
  
    this.paginator.page
      .subscribe(() => this.loadPage());
  }

  filterOnInit() {
    if(this.selectedPlanId && this.selectedForestUnitId) {
      this.loadPage();

      this.forestUnitFilter.load();
      this.divisionFilter.load(this.selectedForestUnitId);
      this.planFilter.load(this.selectedForestUnitId);
    } 
    else {
      this.forestUnitFilter.loadAndFilter();
    }
  }

  loadPage() : void {
    this.dataSource.loadPlanItems(
      this.selectedForestUnitId,
      this.selectedDivisionId,
      this.selectedSubareaId,
      this.selectedPlanId,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }

  filter() : void {
    this.paginator.pageIndex = 0;
    this.loadPage();
  }

  selectedForestUnitChange() : void {
    this.selectedDivisionId = null;
    this.selectedPlanId = null;
    this.selectedSubareaId = null;

    this.filter();

    this.divisionFilter.load(this.selectedForestUnitId);
    this.planFilter.load(this.selectedForestUnitId);
    this.subareaFilter.subareas = [];
  }

  selectedDivisionChange() : void {
    this.selectedSubareaId = null;

    this.filter();

    this.subareaFilter.load(this.selectedDivisionId);
  }

  selectedSubareaChange() : void {
    this.filter();
  }

  selectedPlanChange() : void {
    this.filter();
  }  

  createPlanItemDialog() : void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreatePlanItemComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.loadPage(); 
        }
      }
    ); 
  }

  woodAssortmentsToValue(key : WoodAssortment) : string[] {

    const values : string[] = [];
    let i = 0;
    let x : number;

    while(WoodAssortment[x = 1 << i++]) {
      if(key & x) {
        values.push(WoodAssortment[x]);
      }
    }

    return values;
  }
}
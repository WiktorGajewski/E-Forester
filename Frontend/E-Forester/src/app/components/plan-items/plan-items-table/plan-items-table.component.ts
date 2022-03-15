import { animate, state, style, transition, trigger } from '@angular/animations';
import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, Component, EventEmitter, Inject, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable } from '@angular/material/table';
import { ActionGroup, IPlanItem, WoodAssortment } from 'src/app/models/plan-item.model';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';
import { PlanItemsDataSource } from 'src/app/services/plan-items/plan-items.data-source';
import { CollectionViewer } from "@angular/cdk/collections";
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-plan-items-table',
  templateUrl: './plan-items-table.component.html',
  styleUrls: ['./plan-items-table.component.css'],
  animations: [
    trigger('expandRow', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class PlanItemsTableComponent implements OnInit, AfterViewInit, OnChanges {
  dataSource !: PlanItemsDataSource;
  displayedColumns = [ "address", "plannedHectares", "executedHectares", "plannedCubicMeters", "harvestedCubicMeters", "woodAssortment", "actionGroup", "difficultyLevel", "factor", "isCompleted"];
  data : IPlanItem[] = [];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  @Input() selectedForestUnitId: number | null = null;
  @Input() selectedDivisionId: number | null = null;
  @Input() selectedSubareaId: number | null = null;
  @Input() selectedPlanId: number | null = null;
  @Input() filterByNotCompleted: boolean = false;

  @Output() selectionChange = new EventEmitter<boolean>();

  expandedElement: IPlanItem | null = null;
  expandedElementPlanItemId: number | null = null;

  selection = new SelectionModel<IPlanItem>(true, []);
  
  actionGroups = ActionGroup;
    
  constructor(private planItemService: PlanItemService,
    private authService: AuthService) {

      this.authService.authentication.subscribe(auth => 
        {
          if(auth?.userRole == 1) {
            this.displayedColumns = [ "select", "address", "plannedHectares", "executedHectares", "plannedCubicMeters", "harvestedCubicMeters", "woodAssortment", "actionGroup", "difficultyLevel", "factor", "isCompleted"];
          }
        });
  }

  ngOnInit(): void {
    this.dataSource = new PlanItemsDataSource(this.planItemService);

    if(this.selectedForestUnitId != null || this.selectedPlanId != null) {
      this.dataSource.loadPlanItems(
        this.selectedForestUnitId,
        this.selectedDivisionId,
        this.selectedSubareaId,
        this.selectedPlanId
      );
    }

    this.dataSource.data.subscribe({
        next: data => { this.data = data; }
      });
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .subscribe(() => this.reloadTable());
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.dataSource){
      this.reloadTable();
    }
  }

  reloadTable() : void {
    this.dataSource.loadPlanItems(
      this.selectedForestUnitId,
      this.selectedDivisionId,
      this.selectedSubareaId,
      this.selectedPlanId,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize,
      this.filterByNotCompleted
    );

    this.selection.clear();
    this.selectionChangedEmit();
  }

  isAllSelected() : boolean {
    const numSelected = this.selection.selected.length;
    const numRows = this.data.length;
    return numSelected === numRows;
  }

  masterToggle() : void {
    if(this.isAllSelected()) {
      this.selection.clear();
      return;
    }
    this.selection.select(...this.data);
  }

  selectionChangedEmit() : void {
    this.selectionChange.emit(this.selection.hasValue());
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

  expandElement(row: IPlanItem) : void {
    this.expandedElement = this.expandedElement === row ? null : row;
    this.expandedElementPlanItemId = this.expandedElement?.id ? this.expandedElement.id : null;
  }
}
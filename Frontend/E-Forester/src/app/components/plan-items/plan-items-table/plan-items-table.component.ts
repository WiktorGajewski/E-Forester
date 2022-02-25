import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ActionGroup, IPlanItem, WoodAssortment } from 'src/app/models/plan-item.model';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';
import { PlanItemsDataSource } from 'src/app/services/plan-items/plan-items.data-source';

@Component({
  selector: 'app-plan-items-table',
  templateUrl: './plan-items-table.component.html',
  styleUrls: ['./plan-items-table.component.css'],
  animations: [
    trigger('expendRow', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class PlanItemsTableComponent implements OnInit, AfterViewInit, OnChanges {
  dataSource !: PlanItemsDataSource;
  displayedColumns = ["address", "plannedHectares", "executedHectares", "plannedCubicMeters", "harvestedCubicMeters", "woodAssortment", "actionGroup", "difficultyLevel", "factor", "isCompleted"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  @Input() selectedForestUnitId: number | null = null;
  @Input() selectedDivisionId: number | null = null;
  @Input() selectedSubareaId: number | null = null;
  @Input() selectedPlanId: number | null = null;

  expandedElement: IPlanItem | null = null;
  expandedElementPlanItemId: number | null = null;

  actionGroups = ActionGroup;
    
  constructor(private planItemService: PlanItemService) {}

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
      this.paginator.pageSize
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

  expandElement(row: IPlanItem) : void {
    this.expandedElement = this.expandedElement === row ? null : row;
    this.expandedElementPlanItemId = this.expandedElement?.id ? this.expandedElement.id : null;
  }
}
import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IPage } from "src/app/models/page.model";
import { IPlanItem } from "src/app/models/plan-item.model";
import { PlanItemService } from "./plan-item.service";

export class PlanItemsDataSource implements DataSource<IPlanItem> {

    private planItemsSubject = new BehaviorSubject<IPlanItem[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalCountSubject = new BehaviorSubject<number>(0);

    public loading = this.loadingSubject.asObservable();
    public totalCount = this.totalCountSubject.asObservable();

    constructor(private planItemService : PlanItemService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IPlanItem[]> {
        return this.planItemsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.planItemsSubject.complete();
        this.loadingSubject.complete();
        this.totalCountSubject.complete();
    }

    loadPlanItems(forestUnitId: number|undefined = undefined, divisionId: number|undefined = undefined,
            subareaId: number|undefined = undefined, planId: number | undefined = undefined,
            pageIndex = 1, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.planItemService.getPlanItems(forestUnitId, divisionId, subareaId, planId, pageIndex, pageSize)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPage<IPlanItem>) => {
                    this.planItemsSubject.next(value.data);
                    this.totalCountSubject.next(value.totalCount);
                }
            });
    }
}
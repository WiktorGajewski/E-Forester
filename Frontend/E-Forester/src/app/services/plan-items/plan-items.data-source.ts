import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IPlanItem } from "src/app/models/plan-item.model";
import { PlanItemService } from "./plan-item.service";

export class PlanItemsDataSource implements DataSource<IPlanItem> {

    private planItemsSubject = new BehaviorSubject<IPlanItem[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading = this.loadingSubject.asObservable();

    constructor(private planItemService : PlanItemService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IPlanItem[]> {
        return this.planItemsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.planItemsSubject.complete();
        this.loadingSubject.complete();
    }

    loadPlanItems(filter = "", pageIndex = 0, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.planItemService.getPlanItems(undefined)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPlanItem[]) => {
                    this.planItemsSubject.next(value);
                }
            });
    }
}
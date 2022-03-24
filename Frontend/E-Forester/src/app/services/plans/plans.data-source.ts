import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IPage } from "src/app/models/page.model";
import { IPlan } from "src/app/models/plan.model";
import { PlanService } from "./plan.service";

export class PlansDataSource implements DataSource<IPlan> {

    private plansSubject = new BehaviorSubject<IPlan[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalCountSubject = new BehaviorSubject<number>(0);

    public loading = this.loadingSubject.asObservable();
    public totalCount = this.totalCountSubject.asObservable();

    constructor(private planService : PlanService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IPlan[]> {
        return this.plansSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.plansSubject.complete();
        this.loadingSubject.complete();
        this.totalCountSubject.complete();
    }

    loadPlans(forestUnitId: number | null = null, pageIndex = 1, pageSize = 5) : void {

        this.loadingSubject.next(true);

        this.planService.getPlans(forestUnitId, pageIndex, pageSize, null, null)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPage<IPlan>) => {
                    this.plansSubject.next(value.data);
                    this.totalCountSubject.next(value.totalCount);
                }
            });
    }
}
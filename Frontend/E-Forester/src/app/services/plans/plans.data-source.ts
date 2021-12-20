import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IPlan } from "src/app/models/plan.model";
import { PlanService } from "./plan.service";

export class PlansDataSource implements DataSource<IPlan> {

    private plansSubject = new BehaviorSubject<IPlan[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading = this.loadingSubject.asObservable();

    constructor(private planService : PlanService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IPlan[]> {
        return this.plansSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.plansSubject.complete();
        this.loadingSubject.complete();
    }

    loadPlans(filter = "", pageIndex = 0, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.planService.getPlans()
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPlan[]) => {
                    this.plansSubject.next(value);
                }
            });
    }
}
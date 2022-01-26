import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IPage } from "src/app/models/page.model";
import { IPlanExecution } from "src/app/models/plan-execution.model";
import { PlanExecutionService } from "./plan-execution.service";

export class PlanExecutionsDataSource implements DataSource<IPlanExecution> {

    private planExecutionsSubject = new BehaviorSubject<IPlanExecution[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalCountSubject = new BehaviorSubject<number>(0);

    public loading = this.loadingSubject.asObservable();
    public totalCount = this.totalCountSubject.asObservable();

    constructor(private planExecutionService : PlanExecutionService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IPlanExecution[]> {
        return this.planExecutionsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.planExecutionsSubject.complete();
        this.loadingSubject.complete();
    }

    loadPlanExecutions(pageIndex = 0, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.planExecutionService.getPlanExecutions(pageIndex, pageSize)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPage<IPlanExecution>) => {
                    this.planExecutionsSubject.next(value.data);
                    this.totalCountSubject.next(value.totalCount);
                }
            });
    }
}
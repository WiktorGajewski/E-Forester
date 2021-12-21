import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IPlanExecution } from "src/app/models/plan-execution.model";
import { PlanExecutionService } from "./plan-execution.service";

export class PlanExecutionsDataSource implements DataSource<IPlanExecution> {

    private planExecutionsSubject = new BehaviorSubject<IPlanExecution[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading = this.loadingSubject.asObservable();

    constructor(private planExecutionService : PlanExecutionService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IPlanExecution[]> {
        return this.planExecutionsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.planExecutionsSubject.complete();
        this.loadingSubject.complete();
    }

    loadPlanExecutions(filter = "", pageIndex = 0, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.planExecutionService.getPlanExecutions()
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPlanExecution[]) => {
                    this.planExecutionsSubject.next(value);
                }
            });
    }
}
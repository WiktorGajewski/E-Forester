import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IPage } from "src/app/models/page.model";
import { ISubarea } from "src/app/models/subarea.model";
import { SubareaService } from "./subarea.service";

export class SubareasDataSource implements DataSource<ISubarea> {

    private subareasSubject = new BehaviorSubject<ISubarea[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalCountSubject = new BehaviorSubject<number>(0);

    public loading = this.loadingSubject.asObservable();
    public totalCount = this.totalCountSubject.asObservable();

    constructor(private subareaService : SubareaService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly ISubarea[]> {
        return this.subareasSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.subareasSubject.complete();
        this.loadingSubject.complete();
        this.totalCountSubject.complete();
    }

    loadSubareas(divisionId: number | undefined = undefined, pageIndex = 1, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.subareaService.getSubareas(divisionId, pageIndex, pageSize)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPage<ISubarea>) => {
                    this.subareasSubject.next(value.data);
                    this.totalCountSubject.next(value.totalCount);
                }
            });
    }
}
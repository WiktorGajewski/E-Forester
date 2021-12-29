import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { ISubarea } from "src/app/models/subarea.model";
import { SubareaService } from "./subarea.service";

export class SubareasDataSource implements DataSource<ISubarea> {

    private subareasSubject = new BehaviorSubject<ISubarea[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading = this.loadingSubject.asObservable();

    constructor(private subareaService : SubareaService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly ISubarea[]> {
        return this.subareasSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.subareasSubject.complete();
        this.loadingSubject.complete();
    }

    loadSubareas(filter = "", sortDirection = "", pageIndex = 0, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.subareaService.getSubareas(undefined)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: ISubarea[]) => {
                    this.subareasSubject.next(value);
                }
            });
    }
}
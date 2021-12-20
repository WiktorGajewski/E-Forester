import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, delay, finalize, Observable } from "rxjs";
import { ISubarea } from "src/app/models/subarea.model";
import { SubareaService } from "./subarea.service";

export class SubareaDataSource implements DataSource<ISubarea> {

    private subareaSubject = new BehaviorSubject<ISubarea[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading = this.loadingSubject.asObservable();

    constructor(private subareaService : SubareaService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly ISubarea[]> {
        return this.subareaSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.subareaSubject.complete();
        this.loadingSubject.complete();
    }

    loadSubareas(filter = "", sortDirection = "asc", pageIndex = 0, pageSize = 3) : void {

        this.loadingSubject.next(true);

        this.subareaService.getSubareas()
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: ISubarea[]) => {
                    this.subareaSubject.next(value);
                }
            });
    }
}
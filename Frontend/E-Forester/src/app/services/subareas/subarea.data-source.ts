import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, Observable } from "rxjs";
import { ISubarea } from "src/app/models/subarea.model";
import { SubareaService } from "./subarea.service";

export class SubareaDataSource implements DataSource<ISubarea> {

    private subareaSubject = new BehaviorSubject<ISubarea[]>([]);

    constructor(private subareaService : SubareaService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly ISubarea[]> {
        return this.subareaSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.subareaSubject.complete();
    }

    loadSubareas() : void {
        this.subareaService.getSubareas()
            .subscribe({
                next: (value: ISubarea[]) => {
                    this.subareaSubject.next(value);
                }
            });
    }
}
import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IForestUnit } from "src/app/models/forest-unit.model";
import { ForestUnitService } from "./forest-unit.service";

export class ForestUnitsDataSource implements DataSource<IForestUnit> {

    private forestUnitsSubject = new BehaviorSubject<IForestUnit[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading = this.loadingSubject.asObservable();

    constructor(private forestUnitService : ForestUnitService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IForestUnit[]> {
        return this.forestUnitsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.forestUnitsSubject.complete();
        this.loadingSubject.complete();
    }

    loadForestUnits(filter = "", sortDirection = "", pageIndex = 0, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.forestUnitService.getForestUnits()
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IForestUnit[]) => {
                    this.forestUnitsSubject.next(value);
                }
            });
    }
}
import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IForestUnit } from "src/app/models/forest-unit.model";
import { IPage } from "src/app/models/page.model";
import { ForestUnitService } from "./forest-unit.service";

export class ForestUnitsDataSource implements DataSource<IForestUnit> {

    private forestUnitsSubject = new BehaviorSubject<IForestUnit[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalCountSubject = new BehaviorSubject<number>(0);

    public loading = this.loadingSubject.asObservable();
    public totalCount = this.totalCountSubject.asObservable();

    constructor(private forestUnitService : ForestUnitService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IForestUnit[]> {
        return this.forestUnitsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.forestUnitsSubject.complete();
        this.loadingSubject.complete();
        this.totalCountSubject.complete();
    }

    loadForestUnits(pageIndex = 1, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.forestUnitService.getForestUnits(pageIndex, pageSize)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPage<IForestUnit>) => {
                    this.forestUnitsSubject.next(value.data);
                    this.totalCountSubject.next(value.totalCount);
                }
            });
    }
}
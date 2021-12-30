import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IDivision } from "src/app/models/division.model";
import { IPage } from "src/app/models/page.model";
import { DivisionService } from "./division.service";

export class DivisionsDataSource implements DataSource<IDivision> {

    private divisionsSubject = new BehaviorSubject<IDivision[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalCountSubject = new BehaviorSubject<number>(0);

    public loading = this.loadingSubject.asObservable();
    public totalCount = this.totalCountSubject.asObservable();

    constructor(private divisionService : DivisionService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IDivision[]> {
        return this.divisionsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.divisionsSubject.complete();
        this.loadingSubject.complete();
        this.totalCountSubject.complete();
    }

    loadDivisions(forestUnitId : number | undefined = undefined, sortDirection = "",
             pageIndex = 0, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.divisionService.getDivisions(forestUnitId, pageIndex, pageSize)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPage<IDivision>) => {
                    this.divisionsSubject.next(value.data);
                    this.totalCountSubject.next(value.totalCount);
                }
            });
    }
}
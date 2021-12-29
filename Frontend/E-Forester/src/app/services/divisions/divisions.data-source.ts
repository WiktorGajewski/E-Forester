import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IDivision } from "src/app/models/division.model";
import { DivisionService } from "./division.service";

export class DivisionsDataSource implements DataSource<IDivision> {

    private divisionsSubject = new BehaviorSubject<IDivision[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading = this.loadingSubject.asObservable();

    constructor(private divisionService : DivisionService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IDivision[]> {
        return this.divisionsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.divisionsSubject.complete();
        this.loadingSubject.complete();
    }

    loadDivisions(filter = "", sortDirection = "", pageIndex = 0, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.divisionService.getDivisions(undefined)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IDivision[]) => {
                    this.divisionsSubject.next(value);
                }
            });
    }
}
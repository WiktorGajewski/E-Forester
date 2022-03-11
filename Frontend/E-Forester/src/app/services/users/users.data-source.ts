import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, finalize, Observable } from "rxjs";
import { IPage } from "src/app/models/page.model";
import { IUser } from "src/app/models/user.model";
import { UserService } from "./user.service";

export class UsersDataSource implements DataSource<IUser> {

    private usersSubject = new BehaviorSubject<IUser[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalCountSubject = new BehaviorSubject<number>(0);

    public loading = this.loadingSubject.asObservable();
    public totalCount = this.totalCountSubject.asObservable();

    constructor(private userService : UserService) {

    }

    connect(collectionViewer: CollectionViewer): Observable<readonly IUser[]> {
        return this.usersSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.usersSubject.complete();
        this.loadingSubject.complete();
        this.totalCountSubject.complete();
    }

    loadUsers(pageIndex = 1, pageSize = 10) : void {

        this.loadingSubject.next(true);

        this.userService.getUsers(pageIndex, pageSize)
            .pipe(finalize(() => this.loadingSubject.next(false)))
            .subscribe({
                next: (value: IPage<IUser>) => {
                    this.usersSubject.next(value.data);
                    this.totalCountSubject.next(value.totalCount);
                }
            });
    }
}
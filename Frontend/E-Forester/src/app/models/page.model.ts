export interface IPage<T> {
    pageIndex: number,
    pageSize: number,
    totalCount: number,
    data: T[]
}
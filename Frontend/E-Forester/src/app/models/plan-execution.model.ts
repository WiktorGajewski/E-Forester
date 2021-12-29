export interface IPlanExecution {
    id: number,
    quantity: number,
    createdAt: Date,
    planItemId: number,
    planId: number,
    creatorId: number
}
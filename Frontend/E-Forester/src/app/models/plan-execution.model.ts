export interface IPlanExecution {
    id: number,
    executedHectares: number,
    harvestedCubicMeters: number,
    createdAt: Date,
    planItemId: number,
    planId: number,
    creatorId: number
}
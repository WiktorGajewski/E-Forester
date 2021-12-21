export interface IPlanExecution {
    Id: number,
    Quantity: number,
    CreatedAt: Date,
    PlanItemId: number,
    PlanId: number,
    CreatorId: number
}
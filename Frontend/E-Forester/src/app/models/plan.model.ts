export interface IPlan {
    id: number,
    year: number,
    isComplete: number,
    createdAt: Date,
    forestUnitId: number,
    creatorId: number,
    forestUnitName: string,
    plannedHectares: number,
    plannedCubicMeters: number
    executedHectares: number,
    harvestedCubicMeters: number
}
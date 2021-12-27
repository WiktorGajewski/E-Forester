export interface IPlanItem {
    Id: number,
    IsCompleted: boolean,
    Quantity: number,
    MeasureUnit: string,
    Assortments: WoodAssortment,
    ActionGroup: ActionGroup,
    DifficultyLevel: number,
    Factor: number
    CreatedAt: Date,
    PlanId: number,
    SubareaId: number,
    CreatorId: number
}

export enum WoodAssortment {
    None = 0,       //0
    W = 1 << 0,     //1
    S = 1 << 1,     //2
    M = 1 << 2      //4
}

export enum ActionGroup {
    TWP = 0,
    TPP = 1,
    PTP = 2,
    PTW = 3,
    Rb_I = 4,
    Rb_II = 5,
    Rb_III = 6,
    Rb_IV = 7
}
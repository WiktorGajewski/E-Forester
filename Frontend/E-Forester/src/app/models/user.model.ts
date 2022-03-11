import { IForestUnit } from "./forest-unit.model";

export interface IUser {
    id: number,
    name: string,
    registrationDate: Date,
    role: Role,
    isActive: boolean,
    assignedForestUnits: IForestUnit[]
}

export enum Role {
    Leśniczy = 0,
    Administrator = 1
}
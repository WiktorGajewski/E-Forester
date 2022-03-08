export interface IUser {
    id: number,
    name: string,
    registrationDate: Date,
    role: Role,
    isActive: boolean
}

export enum Role {
    Leśniczy = 0,
    Adminstrator = 1
}
import { Role } from "./user.model";

export interface IAuthentication {
    accessToken: string,
    userRole: Role
}
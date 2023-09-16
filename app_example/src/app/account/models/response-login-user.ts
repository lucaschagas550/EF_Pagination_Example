import { UserToken } from "./user-token";

export interface ResponseLoginUser {
    accessToken: string;
    refreshToken: string;
    expiresIn: number;
    userToken: UserToken;
}

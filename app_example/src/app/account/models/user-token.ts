import { UserClaims } from "./user-claims";

export interface UserToken {
    id: string;
    email: string;
    claims: UserClaims[];
}

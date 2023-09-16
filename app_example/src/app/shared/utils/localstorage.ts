import { UserToken } from 'src/app/account/models/user-token';
import { ResponseLoginUser } from './../../account/models/response-login-user';

export class LocalStorageUtils {

    public getUser(): UserToken {
        const user = localStorage.getItem('user');
        return user ? JSON.parse(user) : null;
    }

    public saveLocalUserData(ResponseLoginUser: ResponseLoginUser): void {
        this.saveUserToken(ResponseLoginUser.accessToken);
        this.saveRefreshToken(ResponseLoginUser.refreshToken);
        this.saveUser(ResponseLoginUser.userToken);
    }

    public clearLocalUserData(): void {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        localStorage.removeItem('refreshToken');
    }

    public getUserToken(): string {
        return localStorage.getItem('token') || '';
    }

    public getRefreshToken(): string {
        return localStorage.getItem('refreshToken') || '';
    }

    public saveUserToken(token: string): void {
        localStorage.setItem('token', token);
    }

    public saveRefreshToken(refreshToken: string): void {
        localStorage.setItem('refreshToken', refreshToken);
    }

    public saveUser(user: UserToken): void {
        localStorage.setItem('user', JSON.stringify(user));
    }
}
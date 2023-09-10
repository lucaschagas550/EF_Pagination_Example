export class LocalStorageUtils {

    //Ajustar para retornar um usuario que sera armazenado
    public getUser(): any {
        const user = localStorage.getItem('user');
        return user ? JSON.parse(user) : null;
    }

    //Implementar quando tiver a tipagem correta dos usuarios e for fazer login do usuario
    public saveLocalUserData(): void {

    }

    public clearLocalUserData(): void {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
    }

    public getUserToken(): string {
        return localStorage.getItem('token') || '';
    }

    public saveUserToken(token: string): void {
        localStorage.setItem('token', token);
    }

    public saveUser(user: string): void {
        localStorage.setItem('user', JSON.stringify(user));
    }
}
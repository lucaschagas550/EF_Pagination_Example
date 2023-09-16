import { UserToken } from './../../account/models/user-token';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageUtils } from 'src/app/shared/utils/localstorage';

@Component({
  selector: 'app-login',
  templateUrl: './menu-login.component.html',
  styleUrls: ['./menu-login.component.css']
})
export class LoginComponent {

  token!: string;
  user!: UserToken;
  localStorageUtils = new LocalStorageUtils();

  constructor(private router: Router) { }

  isLoggedIn(): boolean {
    this.token = this.localStorageUtils.getUserToken();
    this.user = this.localStorageUtils.getUser();

    return (this.token !== null && this.token !== '');
  }

  logout(): void {
    this.localStorageUtils.clearLocalUserData();
    this.router.navigate(['/home']);
  }

}

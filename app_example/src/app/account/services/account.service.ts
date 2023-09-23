import { ResponseLoginUser } from './../models/response-login-user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/services/base.service';
import { LoginUser } from '../models/login-user';
import { Observable, catchError, map } from 'rxjs';
import { RegisterUser } from '../models/register-user';
import { ResponseRegisterUser } from '../models/response-register-user';

@Injectable()
export class AccountService extends BaseService {

  constructor(private http: HttpClient) { super(); }

  login(loginUser: LoginUser): Observable<ResponseLoginUser> {

    let response = this.http
      .post(`${this.UrlService}/auth/login`, loginUser, {
        headers: this.getHeaderJson(),
        observe: 'body',
        responseType: 'json',
      })
      .pipe(
        map(this.extractData),
        catchError(this.serviceError));

    return response;
  }

  register(registerUser: RegisterUser): Observable<ResponseRegisterUser> {

    let response = this.http
      .post(`${this.UrlService}/auth/Register`, registerUser, {
        headers: this.getHeaderJson(),
        observe: 'body',
        responseType: 'json',
      })
      .pipe(
        map(this.extractData),
        catchError(this.serviceError));

    return response;
  }
}

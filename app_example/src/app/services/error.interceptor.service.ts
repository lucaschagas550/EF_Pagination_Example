import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocalStorageUtils } from '../shared/utils/localstorage';
import { Router } from '@angular/router';

import { Observable, catchError, throwError } from 'rxjs';


@Injectable()
export class ErrorInterceptorService implements HttpInterceptor {

  localStorageUtil: LocalStorageUtils = new LocalStorageUtils();

  constructor(private router: Router) { }

  //intercepta a requisicao
  //tem outro exemplo no projeto fluxo de navegacao
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(req).pipe(catchError(error => {
      if (error instanceof HttpErrorResponse) {
        if (error.status === 401) {
          //Para Fazer => Verificar se tem o refresh token no localstore, se tiver tentar realizar o login no endpoint RefreshToken e se for sucesso ja retornar a rota sem ir a tela de login
          this.localStorageUtil.clearLocalUserData();
          this.router.navigate(['/conta/login'], { queryParams: { returnUrl: this.router.url } }); // salva no query param, a url para retornar depois de realizar login
        }
        if (error.status === 403) {
          this.router.navigate(['/acesso-negado']);
        }
      }

      console.log(`Interceptor: ${error}`);
      return throwError(() => error);
    }));
  }
}
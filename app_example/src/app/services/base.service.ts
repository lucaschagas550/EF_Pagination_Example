import { environment } from 'src/environments/environment.prod';
import { LocalStorageUtils } from '../shared/utils/localstorage';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';

//Por ser uma servico abstrato nao precisa do Injectable
export abstract class BaseService {
  protected UrlService: string = environment.apiUrlv1;
  public LocalStorage = new LocalStorageUtils();

  protected getHeaderJson(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json'
    });
  }

  protected getAuthHeaderJson(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.LocalStorage.getUserToken()}`
    });
  };

  protected extractData(response: any): any {
    return response.data ?? {}; // retorna um objeto vazio
  }

  protected serviceError(response: Response | any): Observable<never> { //Observable<never> sempre resulta em emitir um valor de erro, nunca de sucesso
    if (response instanceof HttpErrorResponse) {
      let customError: string[] = [];

      // Você pode adicionar tratamento personalizado para diferentes tipos de erros aqui, com response.status ou response.statuText
      if (response.statusText === "Unknown Error") {
        customError.push("Ocorreu um erro desconhecido");
        response.error.errors = customError;
      }
    }

    console.error('Base service service error final =>', response);
    return throwError(() => response);
  }
}

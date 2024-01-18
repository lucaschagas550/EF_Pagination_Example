import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map } from 'rxjs';
import { BaseService } from 'src/app/services/base.service';
import { Category } from '../models/category';
import { PaginationListParameters } from 'src/app/shared/utils/pagination-list-parameters';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getCategories(paginationListParameters: PaginationListParameters): Observable<Category[]> {

    let response = this.http
      .get(`${this.UrlService}/category`, {
        headers: super.getAuthHeaderJson(),
        params: paginationListParameters.getHttpParams(),
      })
      .pipe(
        map(this.extractList),
        catchError(this.serviceError));

    // let response = this.http
    //   .get(`${this.UrlService}/category?${params}`, {
    //     headers: super.getAuthHeaderJson(),
    //     params: {},
    //   })
    //   .pipe(
    //     map(this.extractList),
    //     catchError(this.serviceError));

    return response;
  }
}

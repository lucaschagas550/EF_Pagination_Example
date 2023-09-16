import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { NavigationModule } from './navigation/navigation.module';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ErrorInterceptorService } from './services/error.interceptor.service';

//Interceptor vai ser passado para classe ErrorInterceptor que eh o error.handler.service
export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true },
];

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NavigationModule,
    HttpClientModule,
    NgbModule, // Bootstrap para angular
    BrowserAnimationsModule, // Modulo de animacao utilizado pelo Toastr
    ToastrModule.forRoot(), // Toastr exibe mensagem ao usuario na tela, pela aplicacao toda por isso esta sendo resolvido neste modulo
  ],
  providers: [httpInterceptorProviders], //Interceptor de request
  bootstrap: [AppComponent]
})
export class AppModule { }

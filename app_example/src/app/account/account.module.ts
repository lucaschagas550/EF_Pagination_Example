import { AccountService } from './services/account.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './login/login.component';
import { AccountComponent } from './account.component';

import { NarikCustomValidatorsModule } from '@narik/custom-validators';


@NgModule({
  declarations: [
    LoginComponent,
    AccountComponent,
  ],
  imports: [
    CommonModule,
    RouterModule, //Para roteamento
    AccountRoutingModule, //Roteamento do modulo
    FormsModule, // Para cadastro de formulario
    ReactiveFormsModule, // Para cadastro de formulario reativos
    HttpClientModule, // Para comunicacao com backend via HTTP
    NarikCustomValidatorsModule, //Pacote externo de validacoes para campos de formulario
  ],
  exports: [
    LoginComponent,
    AccountComponent,
  ],
  providers: [
    AccountService,
  ]
})
export class AccountModule { }

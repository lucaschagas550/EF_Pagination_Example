import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './category.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ListComponent } from './list/list.component';
import { CategoryService } from './services/category.service';

import { NarikCustomValidatorsModule } from '@narik/custom-validators';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';


@NgModule({
  declarations: [
    CategoryComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    CategoryRoutingModule,
    RouterModule, //Para roteamento
    FormsModule, // Para cadastro de formulario
    ReactiveFormsModule, // Para cadastro de formulario reativos
    HttpClientModule, // Para comunicacao com backend via HTTP
    NarikCustomValidatorsModule, //Pacote externo de validacoes para campos de formulario
    MatTableModule,
    MatSortModule
  ],
  providers: [
    CategoryService,
  ]
})
export class CategoryModule { }

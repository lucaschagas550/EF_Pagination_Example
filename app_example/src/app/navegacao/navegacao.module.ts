import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './menu/menu.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { NotFoundComponent } from './not-found/not-found.component';

@NgModule({
  declarations: [
    MenuComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    AccessDeniedComponent,
    NotFoundComponent
  ],
  imports: [
    CommonModule,
    NgbModule,
    RouterModule
  ],
  exports: [
    MenuComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    AccessDeniedComponent,
    NotFoundComponent,
  ]
})
export class NavegacaoModule { }

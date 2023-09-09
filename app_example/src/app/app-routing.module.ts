import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './navegacao/home/home.component';
import { AccessDeniedComponent } from './navegacao/access-denied/access-denied.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full' //nome tem que ser indentico
  },
  {
    path: 'home',
    component: HomeComponent,
  },


  {//Rota para acesso negado a algum recurso
    path: 'access-denied',
    component: AccessDeniedComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

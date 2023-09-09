import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './navegacao/home/home.component';
import { AccessDeniedComponent } from './navegacao/access-denied/access-denied.component';
import { NotFoundComponent } from './navegacao/not-found/not-found.component';

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


  {
    path: 'access-denied',
    component: AccessDeniedComponent
  },
  {
    path: 'not-found',
    component: NotFoundComponent
  },
  {//Em caso de Erro 404 chama este component, sempre deixar por ultimo para nao ser chamado por engano
    path: '**',
    component: NotFoundComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

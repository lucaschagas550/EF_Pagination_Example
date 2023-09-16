import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './navigation/home/home.component';
import { AccessDeniedComponent } from './navigation/access-denied/access-denied.component';
import { NotFoundComponent } from './navigation/not-found/not-found.component';

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
  {//lazyload
    path: 'account',
    loadChildren: () => import('./account/account.module')
      .then(module => module.AccountModule)
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

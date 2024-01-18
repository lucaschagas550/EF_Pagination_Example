import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './category.component';
import { ListComponent } from './list/list.component';

const routes: Routes = [{
  path: '', component: CategoryComponent,
  children: [
    {
      path: 'list',
      component: ListComponent
    },
    {
      path: '',
      redirectTo: 'list', // Redireciona para 'list' quando a rota vazia é acessada
      pathMatch: 'full', // Certifique-se de que a correspondência seja exata
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule { }

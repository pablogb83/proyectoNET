import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { NoticiasAddComponent } from './noticias-add/noticias-add.component';
import { NoticiasEditComponent } from './noticias-edit/noticias-edit.component';
import { NoticiasListComponent } from './noticias-list/noticias-list.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: NoticiasListComponent },
      { path: 'add', component: NoticiasAddComponent},
      // { path: 'edit', component: NoticiasEditComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NoticiasRoutingModule { }

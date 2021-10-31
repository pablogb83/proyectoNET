import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { PuertaAddComponent } from './puerta-add/puerta-add.component';
import { PuertaEditComponent } from './puerta-edit/puerta-edit.component';
import { PuertaListComponent } from './puerta-list/puerta-list.component';


const routes: Routes = [{
  path: '',
  component: LayoutComponent,
  children: [
    { path: '', component: PuertaListComponent },
    { path: 'add', component: PuertaAddComponent },
    { path: 'edit', component: PuertaEditComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PuertaRoutingModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from '../shared/layout/layout.component';
import { AdminAddComponent } from './admin-add/admin-add.component';
import { InstAddComponent } from './inst-add/inst-add.component';
import { InstEditComponent } from './inst-edit/inst-edit.component';
import { InstitucionListComponent } from './institucion-list/institucion-list.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: InstitucionListComponent },
      { path: 'add', component: InstAddComponent },
      { path: 'edit', component: InstEditComponent },
      { path: 'addadmin', component: AdminAddComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class InstitucionRoutingModule { }

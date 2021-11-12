import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { RolesAddComponent } from './roles-add/roles-add.component';
import { RolesEditComponent } from './roles-edit/roles-edit.component';
import { RolesComponent } from './roles-list/roles.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: RolesComponent },
      { path: 'add', component: RolesAddComponent},
      { path: 'edit', component: RolesEditComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RolesRoutingModule { }

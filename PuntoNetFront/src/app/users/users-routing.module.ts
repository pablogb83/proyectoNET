import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserListComponent } from './user-list/user-list.component';
import { LayoutComponent } from '../shared/layout/layout.component';
import { RolesAddComponent } from '../roles/roles-add/roles-add.component';
import { RolesEditComponent } from '../roles/roles-edit/roles-edit.component';
import { UsersAddComponent } from './users-add/users-add.component';
import { UsersEditComponent } from './users-edit/users-edit.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: UserListComponent },
      { path: 'add', component: UsersAddComponent},
      { path: 'edit', component: UsersEditComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }

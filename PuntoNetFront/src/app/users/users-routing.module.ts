import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UserListComponent } from './user-list/user-list.component';
import { LayoutComponent } from '../shared/layout/layout.component';
import { UserRoleComponent } from './user-role/user-role.component';
import { UsersEditComponent } from './users-edit/users-edit.component';
import { UserEdificioComponent } from './user-edificio/user-edificio.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: UserListComponent },
      { path: 'addrole', component: UserRoleComponent },
      { path: 'edit', component: UsersEditComponent },
      { path: 'addedificio', component: UserEdificioComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }

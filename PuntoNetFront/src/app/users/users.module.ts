import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UserListComponent } from './user-list/user-list.component';
import { SharedModule } from '../shared/shared.module';
import { UserRoleComponent } from './user-role/user-role.component';
import { UsersEditComponent } from './users-edit/users-edit.component';
import { UsersAddComponent } from './users-add/users-add.component';
import { UserEdificioComponent } from './user-edificio/user-edificio.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    UsersRoutingModule,
  ],
  declarations: [UserListComponent, UserRoleComponent,UsersEditComponent, UsersAddComponent, UserEdificioComponent]
  
})
export class UsersModule { }

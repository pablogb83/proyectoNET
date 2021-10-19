import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UserListComponent } from './user-list/user-list.component';
import { SharedModule } from '../shared/shared.module';
import { UsersEditComponent } from './users-edit/users-edit.component';
import { UsersAddComponent } from './users-add/users-add.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    UsersRoutingModule
  ],
  declarations: [UserListComponent, UsersEditComponent, UsersAddComponent]
})
export class UsersModule { }

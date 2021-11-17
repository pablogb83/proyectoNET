import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InstitucionRoutingModule } from './institucion-routing.module';
import { InstitucionListComponent } from './institucion-list/institucion-list.component';
import { SharedModule } from '../shared/shared.module';
import { InstAddComponent } from './inst-add/inst-add.component';
import { InstEditComponent } from './inst-edit/inst-edit.component';
import { UsersAddComponent } from '../users/users-add/users-add.component';
import { AdminAddComponent } from './admin-add/admin-add.component';



@NgModule({
  declarations: [InstitucionListComponent, InstAddComponent, InstEditComponent, UsersAddComponent, AdminAddComponent],
  imports: [
    CommonModule,
    SharedModule,
    InstitucionRoutingModule
  ]
})
export class InstitucionModule { }

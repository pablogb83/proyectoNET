import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RolesRoutingModule } from './roles-routing.module';
import { RolesAddComponent } from './roles-add/roles-add.component';
import { RolesEditComponent } from './roles-edit/roles-edit.component';
import { SharedModule } from '../shared/shared.module';
import { RolesComponent } from './roles-list/roles.component';


@NgModule({
  declarations: [RolesComponent, RolesAddComponent, RolesEditComponent],
  imports: [
    CommonModule,
    SharedModule,
    RolesRoutingModule
  ]
})
export class RolesModule { }

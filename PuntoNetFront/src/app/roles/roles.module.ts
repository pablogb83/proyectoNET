import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RolesRoutingModule } from './roles-routing.module';

import { SharedModule } from '../shared/shared.module';
import { RolesComponent } from './roles-list/roles.component';


@NgModule({
  declarations: [RolesComponent],
  imports: [
    CommonModule,
    SharedModule,
    RolesRoutingModule
  ]
})
export class RolesModule { }

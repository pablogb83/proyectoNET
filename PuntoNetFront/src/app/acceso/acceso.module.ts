import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccesoRoutingModule } from './acceso-routing.module';
import { AccesoListComponent } from './acceso-list/acceso-list.component';
import { AccesoAddComponent } from './acceso-add/acceso-add.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [AccesoListComponent, AccesoAddComponent],
  imports: [
    CommonModule,
    AccesoRoutingModule,
    SharedModule 
  ]
})
export class AccesoModule { }

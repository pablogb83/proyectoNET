import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccesoRoutingModule } from './acceso-routing.module';
import { AccesoListComponent } from './acceso-list/acceso-list.component';
import { AccesoAddComponent } from './acceso-add/acceso-add.component';
import { SharedModule } from '../shared/shared.module';
import { ReconocimientoFacialComponent } from '../reconocimiento-facial/reconocimiento-facial.component';


@NgModule({
  declarations: [AccesoListComponent, AccesoAddComponent,ReconocimientoFacialComponent],
  imports: [
    CommonModule,
    AccesoRoutingModule,
    SharedModule 
  ]
})
export class AccesoModule { }

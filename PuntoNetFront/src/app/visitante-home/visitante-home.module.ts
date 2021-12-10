import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VisitanteHomeRoutingModule } from './visitante-home-routing.module';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    VisitanteHomeRoutingModule,
    SharedModule,
  ]
})
export class VisitanteHomeModule { }

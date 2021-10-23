import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PagoRoutingModule } from './pago-routing.module';
import { PagoComponent } from './pago.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [PagoComponent],
  imports: [
    CommonModule,
    PagoRoutingModule,
    SharedModule
  ]
})
export class PagoModule { }

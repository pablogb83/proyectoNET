import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagoRoutingModule } from './pago-routing.module';
import { PagoComponent } from './pago.component';
import { SharedModule } from '../shared/shared.module';
import { PaypalButtonComponent } from '../paypal-button/paypal-button.component';


@NgModule({
  declarations: [PagoComponent,PaypalButtonComponent],
  imports: [
    CommonModule,
    PagoRoutingModule,
    SharedModule,
  ]
})
export class PagoModule { }

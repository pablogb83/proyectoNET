import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PaypalButtonComponent } from '../paypal-button/paypal-button.component';
import { LayoutComponent } from '../shared/layout/layout.component';
import { PagoComponent } from './pago.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: PagoComponent },
      { path: 'button', component: PaypalButtonComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagoRoutingModule { }

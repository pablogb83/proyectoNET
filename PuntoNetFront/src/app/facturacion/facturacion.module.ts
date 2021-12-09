import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { FacturacionRoutingModule } from './facturacion-routing.module';
import { FacturacionListComponent } from './facturacion-list/facturacion-list.component';
import { FacturacionDetalleComponent } from './facturacion-detalle/facturacion-detalle.component';


@NgModule({
  declarations: [FacturacionListComponent, FacturacionDetalleComponent],
  imports: [
    CommonModule,
    SharedModule,
    FacturacionRoutingModule
  ]
})
export class FacturacionModule { }

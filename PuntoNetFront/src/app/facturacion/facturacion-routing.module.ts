import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { FacturacionDetalleComponent } from './facturacion-detalle/facturacion-detalle.component';
import { FacturacionListComponent } from './facturacion-list/facturacion-list.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: FacturacionListComponent },
      { path: 'detail', component: FacturacionDetalleComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FacturacionRoutingModule { }

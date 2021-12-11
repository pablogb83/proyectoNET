import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductosRoutingModule } from './productos-routing.module';
import { ProductosListComponent } from './productos-list/productos-list.component';
import { ProductosAddComponent } from './productos-add/productos-add.component';
import { SharedModule } from '../shared/shared.module';
import { ProductosEditPriceComponent } from './productos-edit-price/productos-edit-price.component';


@NgModule({
  declarations: [ProductosListComponent,ProductosAddComponent, ProductosEditPriceComponent],
  imports: [
    CommonModule,
    SharedModule,
    ProductosRoutingModule
  ]
})
export class ProductosModule { }

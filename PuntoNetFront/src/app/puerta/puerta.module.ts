import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PuertaRoutingModule } from './puerta-routing.module';
import { PuertaEditComponent } from './puerta-edit/puerta-edit.component';
import { PuertaAddComponent } from './puerta-add/puerta-add.component';
import { PuertaListComponent } from './puerta-list/puerta-list.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [PuertaEditComponent, PuertaAddComponent, PuertaListComponent],
  imports: [
    CommonModule,
    PuertaRoutingModule,
    SharedModule 
  ]
})
export class PuertaModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SalonRoutingModule } from './salon-routing.module';
import { SalonListComponent } from './salon-list/salon-list.component';
import { SharedModule } from '../shared/shared.module';
import { SalonAddComponent } from './salon-add/salon-add.component';
import { SalonEditComponent } from './salon-edit/salon-edit.component';


@NgModule({
  declarations: [SalonListComponent,SalonListComponent, SalonAddComponent, SalonEditComponent],
  imports: [
    CommonModule,
    SalonRoutingModule,
    SharedModule 
  ]
})
export class SalonModule { }

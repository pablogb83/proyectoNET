import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EdificiosRoutingModule } from './edificios-routing.module';
import { EdificiosEditComponent } from './edificios-edit/edificios-edit.component';
import { EdificiosAddComponent } from './edificios-add/edificios-add.component';
import { EdificiosListComponent } from './edificios-list/edificios-list.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [EdificiosEditComponent, EdificiosAddComponent, EdificiosListComponent],
  imports: [
    CommonModule,
    SharedModule,
    EdificiosRoutingModule
  ]
})
export class EdificiosModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PersonaRoutingModule } from './persona-routing.module';
import { PersonaListComponent } from './persona-list/persona-list.component';
import { PersonaAddComponent } from './persona-add/persona-add.component';
import { PersonaEditComponent } from './persona-edit/persona-edit.component';
import { SharedModule } from '../shared/shared.module';
import { PersonaAltaMasivaComponent } from './persona-alta-masiva/persona-alta-masiva.component';


@NgModule({
  declarations: [PersonaListComponent, PersonaAddComponent, PersonaEditComponent, PersonaAltaMasivaComponent],
  imports: [
    CommonModule,
    PersonaRoutingModule,
    SharedModule
  ]
})
export class PersonaModule { }

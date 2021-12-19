import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NoticiasPublicasRoutingModule } from './noticias-publicas-routing.module';
import { SharedModule } from '../shared/shared.module';
import { NoticiasPublicasComponent } from './noticias-publicas-list/noticias-publicas.component';


@NgModule({
  declarations: [
    NoticiasPublicasComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    NoticiasPublicasRoutingModule
  ]
})
export class NoticiasPublicasModule { }

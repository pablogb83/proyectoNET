import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NoticiasRoutingModule } from './noticias-routing.module';
import { NoticiasAddComponent } from './noticias-add/noticias-add.component';
import { NoticiasEditComponent } from './noticias-edit/noticias-edit.component';
import { NoticiasListComponent } from './noticias-list/noticias-list.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    NoticiasAddComponent,
    // NoticiasEditComponent, 
    NoticiasListComponent],
  imports: [
    CommonModule,
    SharedModule,
    NoticiasRoutingModule
  ]
})
export class NoticiasModule { }

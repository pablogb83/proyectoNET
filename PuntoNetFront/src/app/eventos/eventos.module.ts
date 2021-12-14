import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventosRoutingModule } from './eventos-routing.module';
import { EventosAddComponent } from './eventos-add/eventos-add.component';
import { EventosEditComponent } from './eventos-edit/eventos-edit.component';
import { EventosListComponent } from './eventos-list/eventos-list.component';
import { SharedModule } from '../shared/shared.module';
import { NgxMatNativeDateModule } from '@angular-material-components/datetime-picker';


@NgModule({
  declarations: [
    EventosAddComponent,
    EventosEditComponent,
    EventosListComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    EventosRoutingModule
  ],
  providers:[
  ]
})
export class EventosModule { }

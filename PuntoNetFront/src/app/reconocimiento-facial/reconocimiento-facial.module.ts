import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReconocimientoFacialRoutingModule } from './reconocimiento-facial-routing.module';
import { SharedModule } from '../shared/shared.module';
import { WebcamModule } from 'ngx-webcam';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
    ReconocimientoFacialRoutingModule,
    WebcamModule
  ],
  exports:[WebcamModule]
})
export class ReconocimientoFacialModule { }

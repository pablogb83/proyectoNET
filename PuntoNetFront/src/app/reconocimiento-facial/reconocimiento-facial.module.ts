import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebcamComponent, WebcamModule } from 'ngx-webcam';
import { ReconocimientoFacialRoutingModule } from './reconocimiento-facial-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ReconocimientoFacialComponent } from './reconocimiento-facial.component';


@NgModule({
  declarations: [ReconocimientoFacialComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReconocimientoFacialRoutingModule,
    WebcamModule
  ]
})
export class ReconocimientoFacialModule { }

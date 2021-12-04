import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReconocimientoFacialRoutingModule } from './reconocimiento-facial-routing.module';
import { SharedModule } from '../shared/shared.module';
import { WebcamSnapshotComponent } from '../webcam-snapshot/webcam-snapshot.component';


@NgModule({
  declarations: [WebcamSnapshotComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReconocimientoFacialRoutingModule,
  ]
})
export class ReconocimientoFacialModule { }

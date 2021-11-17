import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { WebcamSnapshotComponent } from '../webcam-snapshot/webcam-snapshot.component';
import { ReconocimientoFacialComponent } from './reconocimiento-facial.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: ReconocimientoFacialComponent },
      { path: 'camara', component: WebcamSnapshotComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReconocimientoFacialRoutingModule { }

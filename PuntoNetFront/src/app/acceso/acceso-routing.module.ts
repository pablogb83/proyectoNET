import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReconocimientoFacialComponent } from '../reconocimiento-facial/reconocimiento-facial.component';
import { LayoutComponent } from '../shared/layout/layout.component';
import { AccesoAddComponent } from './acceso-add/acceso-add.component';
import { AccesoListComponent } from './acceso-list/acceso-list.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: AccesoListComponent },
      { path: 'add', component: AccesoAddComponent},
      { path: 'facialrecognition', component: ReconocimientoFacialComponent},

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccesoRoutingModule { }

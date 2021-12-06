import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboradGetNoticiaComponent } from './dashborad-get-noticia/dashborad-get-noticia.component';
import { LayoutComponent } from '../shared/layout/layout.component';
import { DashboardHomeComponent } from './dashboard-home/dashboard-home.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: DashboardHomeComponent },
      { path: 'get', component: DashboradGetNoticiaComponent},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }

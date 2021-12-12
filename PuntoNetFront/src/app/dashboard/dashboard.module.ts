import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule } from '../shared/shared.module';
import { DashboardHomeComponent } from './dashboard-home/dashboard-home.component';
import { DashboradGetNoticiaComponent } from './dashborad-get-noticia/dashborad-get-noticia.component';


@NgModule({
  declarations: [DashboardHomeComponent, DashboradGetNoticiaComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModule
  ],
  entryComponents: []
})
export class DashboardModule { }

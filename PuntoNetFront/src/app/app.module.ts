import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { CustomMaterialModule } from './custom-material/custom-material.module';
import { AppRoutingModule } from './app-routing.module';
import { AdminGuard } from './core/guards/admin.guard';
import { LoggerConfig } from 'ngx-logger';
import { DatePipe } from '@angular/common';
import { NGXLoggerHttpService, NGXMapperService } from 'ngx-logger';
import { ReconocimientoFacialComponent } from './reconocimiento-facial/reconocimiento-facial.component';
import { UsersAddComponent } from './users/users-add/users-add.component';
import { WebcamModule } from 'ngx-webcam';
import { WebcamSnapshotComponent } from './webcam-snapshot/webcam-snapshot.component';
import { PersonaAddComponent } from './persona/persona-add/persona-add.component';
import { ProductosListComponent } from './productos/productos-list/productos-list.component';
import { ProductosAddComponent } from './productos/productos-add/productos-add.component';
import { FacturacionListComponent } from './facturacion/facturacion-list/facturacion-list.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { CalendarioComponent } from './calendario/calendario.component';



@NgModule({
  declarations: [
    AppComponent,
    ReconocimientoFacialComponent,
    UsersAddComponent,
    WebcamSnapshotComponent,
    PersonaAddComponent,
    CalendarioComponent
  ],
  entryComponents:[
    PersonaAddComponent,
    ReconocimientoFacialComponent,
    UsersAddComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CoreModule,
    SharedModule,
    CustomMaterialModule.forRoot(),
    AppRoutingModule,
    WebcamModule,
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory })
  ],
  bootstrap: [AppComponent],
  providers:[
    AdminGuard,
    LoggerConfig,
    DatePipe,
    NGXMapperService,
    NGXLoggerHttpService,
    AdminGuard
  ],
  exports:[WebcamModule]

})
export class AppModule {
  
 }

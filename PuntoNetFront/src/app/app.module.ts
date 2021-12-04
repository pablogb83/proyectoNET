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



@NgModule({
  declarations: [
    AppComponent,
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
  entryComponents:[
    ReconocimientoFacialComponent,
    UsersAddComponent
  ]

})
export class AppModule {
  
 }

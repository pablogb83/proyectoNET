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
import { LoggerConfig, NGXLogger } from 'ngx-logger';
import { DatePipe } from '@angular/common';


@NgModule({
  declarations: [
    AppComponent
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
    DatePipe
  ]
})
export class AppModule { }

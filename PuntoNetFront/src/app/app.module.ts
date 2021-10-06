import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Login/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { InstitucionComponent } from './institucion/institucion.component';
import { ShowInstComponent } from './institucion/show-inst/show-inst.component';
import { AddEditInstComponent } from './institucion/add-edit-inst/add-edit-inst.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    InstitucionComponent,
    ShowInstComponent,
    AddEditInstComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

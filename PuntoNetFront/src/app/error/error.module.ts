import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ErrorRoutingModule } from './error-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ErrorComponent } from './error-screen/error.component';


@NgModule({
  declarations: [ErrorComponent],
  imports: [
    CommonModule,
    SharedModule,
    ErrorRoutingModule
  ]
})
export class ErrorModule { }

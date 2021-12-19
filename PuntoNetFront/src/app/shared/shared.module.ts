import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { LimitToPipe } from './pipes/limit-to.pipe';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { ContentPlaceholderAnimationComponent } from './content-placeholder-animation/content-placeholder-animation.component';
import { LocalDatePipe } from './pipes/local-date.pipe';
import { YesNoPipe } from './pipes/yes-no.pipe';
import { LayoutComponent } from './layout/layout.component';
import { CustomMaterialModule } from '../custom-material/custom-material.module';
import { CalendarioComponent } from '../calendario/calendario.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
@NgModule({
  imports: [
    RouterModule,
    CustomMaterialModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory })
  ],
  declarations: [
    ConfirmDialogComponent,
    CalendarioComponent,
    ContentPlaceholderAnimationComponent,
    LimitToPipe,
    LocalDatePipe,
    YesNoPipe,
    LayoutComponent
  ],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    CustomMaterialModule,
    LimitToPipe,
    ConfirmDialogComponent,
    ContentPlaceholderAnimationComponent,
    LocalDatePipe,
    YesNoPipe
  ],
  entryComponents: [
    ConfirmDialogComponent
  ]
})
export class SharedModule { }

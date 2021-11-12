import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { EventosAddComponent } from './eventos-add/eventos-add.component';
import { EventosEditComponent } from './eventos-edit/eventos-edit.component';
import { EventosListComponent } from './eventos-list/eventos-list.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: EventosListComponent },
      { path: 'add', component: EventosAddComponent},
      { path: 'edit', component: EventosEditComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EventosRoutingModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { PersonaAltaMasivaComponent } from './persona-alta-masiva/persona-alta-masiva.component';
import { PersonaEditComponent } from './persona-edit/persona-edit.component';
import { PersonaListComponent } from './persona-list/persona-list.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: PersonaListComponent },

      { path: 'edit', component: PersonaEditComponent },
      { path: 'altaMasiva', component: PersonaAltaMasivaComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PersonaRoutingModule { }

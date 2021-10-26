import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { EdificiosAddComponent } from './edificios-add/edificios-add.component';
import { EdificiosEditComponent } from './edificios-edit/edificios-edit.component';
import { EdificiosListComponent } from './edificios-list/edificios-list.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: EdificiosListComponent },
      { path: 'add', component: EdificiosAddComponent},
      { path: 'edit', component: EdificiosEditComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EdificiosRoutingModule { }

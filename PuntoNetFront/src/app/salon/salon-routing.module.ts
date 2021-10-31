import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/layout/layout.component';
import { SalonAddComponent } from './salon-add/salon-add.component';
import { SalonEditComponent } from './salon-edit/salon-edit.component';
import { SalonListComponent } from './salon-list/salon-list.component';


const routes: Routes = [{
  path: '',
  component: LayoutComponent,
  children: [
    { path: '', component: SalonListComponent },
    { path: 'add', component: SalonAddComponent },
    { path: 'edit', component: SalonEditComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalonRoutingModule { }

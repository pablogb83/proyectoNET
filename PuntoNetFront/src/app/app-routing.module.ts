import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccesoModule } from './acceso/acceso.module';
import { AccountModule } from './account/account.module';
import { AuthModule } from './auth/auth.module';

import { CustomersModule } from './customers/customers.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { EdificiosModule } from './edificios/edificios.module';
import { EventosModule } from './eventos/eventos.module';
import { InstitucionModule } from './institucion/institucion.module';
import { PagoModule } from './pago/pago.module';
import { PersonaModule } from './persona/persona.module';
import { PuertaModule } from './puerta/puerta.module';
import { ReconocimientoFacialModule } from './reconocimiento-facial/reconocimiento-facial.module';
import { RolesModule } from './roles/roles.module';
import { SalonModule } from './salon/salon.module';
import { UsersModule } from './users/users.module';
import { AdminGuard } from './core/guards/admin.guard';
import { AdminporteroGuard } from './core/guards/adminportero.guard';

import { AuthGuard } from './core/guards/auth.guard';
import { SuperAdminGuard } from './core/guards/superadmin.guard';
import { PagoComponent } from './pago/pago.component';

const appRoutes: Routes = [
    {
        path: 'auth',
        loadChildren: ()=> AuthModule
    },
    {
        path: 'dashboard',
        loadChildren: () => DashboardModule,
        canActivate: [AuthGuard]
    },
    {
        path: 'customers',
        loadChildren: ()=> CustomersModule,
        canActivate: [AuthGuard]
    },
    {
        path: 'users',
        loadChildren: ()=> UsersModule,
        canActivate: [AuthGuard]
    },
    {
        path: 'eventos',
        loadChildren: () => EventosModule,
        canActivate: [AuthGuard]
    },
    {
        path: 'account',
        loadChildren: ()=> AccountModule,
        canActivate: [AuthGuard]
    },
    {
        path: 'institucion',
        loadChildren: ()=>InstitucionModule,
        canActivate: [AuthGuard, SuperAdminGuard]
    },
    {
        path: 'pago',
        loadChildren: ()=>PagoModule,
        canActivate: [AuthGuard]
    },
    {
        path: 'edificios',
        loadChildren: ()=>EdificiosModule,
        canActivate: [AuthGuard, AdminGuard]
    },
    {
        path: 'roles',
        loadChildren: ()=>RolesModule,
        canActivate: [AuthGuard, SuperAdminGuard]
    },
    {
        path: 'salones',
        loadChildren: ()=>SalonModule,
        canActivate: [AuthGuard, AdminGuard]
    },
    {
        path: 'puertas',
        loadChildren: ()=>PuertaModule,
        canActivate: [AuthGuard/*, AdminGuard*/]
    },
    {
        path: 'personas',
        loadChildren: ()=>PersonaModule,
        canActivate: [AuthGuard/*, AdminGuard*/]
    },
    {
        path: 'reconocimiento-facial',
        loadChildren: ()=>ReconocimientoFacialModule,
    },
    {
        path: 'accesos',
        loadChildren: ()=>AccesoModule,
        canActivate: [AuthGuard/*, AdminGuard*/]
    },
    {
        path: '**',
        redirectTo: 'dashboard',
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes)
    ],
    exports: [RouterModule],
    providers: []
})
export class AppRoutingModule { }

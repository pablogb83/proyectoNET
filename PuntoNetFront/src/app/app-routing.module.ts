import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccesoModule } from './acceso/acceso.module';
import { AuthModule } from './auth/auth.module';

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

import { AuthGuard } from './core/guards/auth.guard';
import { SuperAdminGuard } from './core/guards/superadmin.guard';
import { ProductosModule } from './productos/productos.module';
import { FacturacionModule } from './facturacion/facturacion.module';
import { NoticiasModule } from './noticias/noticias.module';
import { VisitanteHomeModule } from './visitante-home/visitante-home.module';
import { PanelOpcionesModule } from './panel-opciones/panel-opciones.module';
import { AdminSuperadminGuard } from './core/guards/adminsuperadmin.guard';

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
        path: 'users',
        loadChildren: ()=> UsersModule,
        canActivate: [AuthGuard,AdminSuperadminGuard]
    },
    {
        path: 'eventos',
        loadChildren: ()=> EventosModule,
        canActivate: [AuthGuard]
    },
    {
        path: 'noticias',
        loadChildren: ()=> NoticiasModule,
        canActivate: [AuthGuard]
    },
    {
        path: 'institucion',
        loadChildren: ()=>InstitucionModule,
        canActivate: [AuthGuard, SuperAdminGuard]
    },
    {
        path: 'pago',
        loadChildren: ()=>PagoModule
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
        path: 'productos',
        loadChildren: ()=>ProductosModule,
        canActivate: [AuthGuard, SuperAdminGuard]
    },
    {
        path: 'facturacion',
        loadChildren: ()=>FacturacionModule,
        canActivate: [AuthGuard]
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
        path: 'panelOpciones',
        loadChildren: ()=>PanelOpcionesModule,
        canActivate: [AuthGuard/*, AdminGuard*/]
    },
    {
        path: 'visitantehome',
        loadChildren: ()=>VisitanteHomeModule
    },
    {
        path: '**',
        redirectTo: 'visitantehome',
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

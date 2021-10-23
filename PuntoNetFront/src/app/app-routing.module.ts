import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './core/guards/auth.guard';
import { PagoComponent } from './pago/pago.component';

const appRoutes: Routes = [
    {
        path: 'auth',
        loadChildren: './auth/auth.module#AuthModule'
    },
    {
        path: 'dashboard',
        loadChildren: './dashboard/dashboard.module#DashboardModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'customers',
        loadChildren: './customers/customers.module#CustomersModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'users',
        loadChildren: './users/users.module#UsersModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'account',
        loadChildren: './account/account.module#AccountModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'icons',
        loadChildren: './icons/icons.module#IconsModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'typography',
        loadChildren: './typography/typography.module#TypographyModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'about',
        loadChildren: './about/about.module#AboutModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'institucion',
        loadChildren: './institucion/institucion.module#InstitucionModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'pago',
        loadChildren: './pago/pago.module#PagoModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'edificios',
        loadChildren: './edificios/edificios.module#EdificiosModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'roles',
        loadChildren: './roles/roles.module#RolesModule',
        canActivate: [AuthGuard]
    },
    {
        path: 'salones',
        loadChildren: './salon/salon.module#SalonModule',
        canActivate: [AuthGuard]
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

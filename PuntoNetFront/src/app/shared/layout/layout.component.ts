import { Component, OnInit, ChangeDetectorRef, OnDestroy, AfterViewInit } from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';
import { TimerObservable } from 'rxjs/observable/TimerObservable';
import { Subscription } from 'rxjs';

import { environment } from './../../../environments/environment';
import { AuthenticationService } from './../../core/services/auth.service';
import { SpinnerService } from '../../core/services/spinner.service';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { UsuarioEdificioService } from 'src/app/core/services/usuario-edificio.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit, AfterViewInit {

    private _mobileQueryListener: () => void;
    mobileQuery: MediaQueryList;
    showSpinner: boolean;
    userName: string;
    isAdmin: boolean;
    rol:string;
    instActive: boolean;

    idedificio:number;
    idUsuario:string;

    private autoLogoutSubscription: Subscription;

    constructor(private changeDetectorRef: ChangeDetectorRef,
        private media: MediaMatcher,
        public spinnerService: SpinnerService,
        private authService: AuthenticationService,
        private authGuard: AuthGuard,
        private tokenService: TokenStorageService,
        private usuarioEdificio: UsuarioEdificioService,
        private router: Router) {

        this.mobileQuery = this.media.matchMedia('(max-width: 1000px)');
        this._mobileQueryListener = () => changeDetectorRef.detectChanges();
        // tslint:disable-next-line: deprecation
        this.mobileQuery.addListener(this._mobileQueryListener);
    }

    ngOnInit(): void {
        this.userName = this.tokenService.getUserName();
        this.rol = this.tokenService.getRoleName();
        this.instActive = this.tokenService.getStatus();
        this.idUsuario = this.tokenService.getUserId();
        // Auto log-out subscription
        console.log(this.idUsuario);
        if(this.rol==='PORTERO' && this.idUsuario){
            this.usuarioEdificio.getEdificioUsuario(this.idUsuario).subscribe(data=>{
                console.log(data);
                this.idedificio = data.id;
            })
            //this.idedificio = 1;
        }
    }

    ngAfterViewInit(): void {
        this.changeDetectorRef.detectChanges();
    }

    logout(){
        this.tokenService.signOut();
        //window.location.reload();
        this.router.navigate(['auth/login']);
    }
}

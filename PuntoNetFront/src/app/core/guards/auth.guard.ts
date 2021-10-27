import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import * as moment from 'moment';

import { AuthenticationService } from '../services/auth.service';
import { NotificationService } from '../services/notification.service';
import { TokenStorageService } from '../services/token-storage.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router,
        private notificationService: NotificationService,
        private authService: TokenStorageService) { }

    canActivate() {
        const status = Boolean(this.authService.getStatus());
        const user = this.authService.getRoleName();
        if (user && user!=="VISITANTE" &&  status){
            return true;
            /*else {
                this.notificationService.openSnackBar('Your session has expired');
                this.router.navigate(['auth/login']);
                return false;
            }*/
        }
        this.router.navigate(['auth/login']);
        return false;
    }
}

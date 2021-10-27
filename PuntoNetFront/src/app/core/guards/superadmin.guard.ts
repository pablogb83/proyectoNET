import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthenticationService } from '../services/auth.service';
import { TokenStorageService } from '../services/token-storage.service';

@Injectable()
export class SuperAdminGuard implements CanActivate {

    constructor(private router: Router,
        private authService: TokenStorageService) { }

    canActivate() {
        const user = this.authService.getRoleName();

        if (user && user==="SUPERADMIN") {
            return true;

        } else {
            this.router.navigate(['/']);
            return false;
        }
    }
}

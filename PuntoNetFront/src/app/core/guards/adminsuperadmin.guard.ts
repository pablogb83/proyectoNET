import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthenticationService } from '../services/auth.service';
import { TokenStorageService } from '../services/token-storage.service';

@Injectable()
export class AdminSuperadminGuard implements CanActivate {

    constructor(private router: Router,
        private authService: TokenStorageService) { }

    canActivate() {
        const user = this.authService.getRoleName();
        const active = this.authService.getStatus();
      
        if(user && ((user==="ADMIN" && active)||user==="SUPERADMIN")){
            return true;
        }
        if(!active && user && user==="ADMIN"){
            if(this.router.url.includes("pago")){
                return true;
            }
            this.router.navigate(['/pago']);
            return false;
        }
        return false;
    }
}

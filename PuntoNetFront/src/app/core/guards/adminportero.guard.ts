import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenStorageService } from '../services/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AdminporteroGuard implements CanActivate {
  constructor(private router: Router,
    private authService: TokenStorageService) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const user = this.authService.getRoleName();
      const active = this.authService.getStatus();
    
      if(user && active && (user==="ADMIN" || user==="PORTERO")){
          return true;
      }
      if(!active){
          this.router.navigate(['/pago']);
          return false;
      }
      return false;
  }
  
}

import { Injectable } from '@angular/core';

const TOKEN_KEY = 'auth-token';
const ROLE_NAME = 'role';
const USER_NAME = '';
const USER_ID = '';
const STATUS = 'status';
const TENANT = 'tenant';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  constructor() { }

  signOut(): void {
    window.sessionStorage.clear();
  }

  public saveToken(token: string): void {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
  }

  public deleteToken(): void {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.removeItem(ROLE_NAME);
    window.sessionStorage.removeItem(USER_NAME);
  }


  public getToken(): string | null {
    return window.sessionStorage.getItem(TOKEN_KEY);

  }

  public saveRoleName(rol: string): void {
    window.sessionStorage.removeItem(ROLE_NAME);
    window.sessionStorage.setItem(ROLE_NAME, rol);
  }

  public getRoleName(): string | null {
    var tipo = window.sessionStorage.getItem(ROLE_NAME);
    if(!tipo){
      tipo='VISITANTE';
    }
    return tipo;
  }

  public saveUserName(username: string): void {
    window.sessionStorage.removeItem(USER_NAME);
    window.sessionStorage.setItem(USER_NAME, username);
  }

  public getUserName(): string | null {
    return window.sessionStorage.getItem(USER_NAME);
  }

  public saveTenant(tenant: string): void {
    window.sessionStorage.removeItem(TENANT);
    window.sessionStorage.setItem(TENANT, tenant);
  }

  public getTenant(): string | null {
    return window.sessionStorage.getItem(TENANT);
  }


  public saveStatus(status: boolean): void {
    window.sessionStorage.removeItem(STATUS);
    if(status){
      window.sessionStorage.setItem(STATUS, 'ACTIVE');
    }
    else{
      window.sessionStorage.setItem(STATUS, 'INACTIVE');
    }
  }


  public getStatus(): boolean | null {
    return Boolean(window.sessionStorage.getItem(STATUS)==="ACTIVE");
  }

  public saveUserId(id: string): void {
    window.sessionStorage.removeItem(USER_ID);
    window.sessionStorage.setItem(USER_ID, id);
  }

  public getUserId(): string | null {
    return window.sessionStorage.getItem(USER_ID);
  }
}

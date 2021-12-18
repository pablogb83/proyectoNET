import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

const AUTH_API = '/api/usuarios/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})

export class UsuariosService {

  constructor(private http: HttpClient) { }

  getUsuarios(): Observable<any> {
    return this.http.get(AUTH_API);
  }

  getUsuariosAdmin(): Observable<any> {
    return this.http.get(AUTH_API + 'admin');
  }

  getUsuariosInstitucion(institucion: string): Observable<any> {
    return this.http.get(AUTH_API + `institucion/${institucion}`);
  }

  getUsuario(id: number):Observable<any[]>{
    return this.http.get<any>(AUTH_API+id);
  }

  postAdmin(email: string, passwordPlano: string, tenantId?: string){
    return this.http.post(AUTH_API + 'admin' , {
      email,
      passwordPlano,
      tenantId
    },httpOptions);
  }

  postUsuario(email: string, passwordPlano: string){
    return this.http.post(AUTH_API , {
      email,
      passwordPlano
    },httpOptions);
  }

  putUsuario(id:number, email: string, password: string){
     return this.http.put(AUTH_API + id, {
      email,
      password,
     }, httpOptions);
  }

  putAdmin(id:number, email: string, password: string){
    return this.http.put(AUTH_API + `admin/${id}`, {
     email,
     password,
    }, httpOptions);
 }

  deleteUsuario(id:string){
    return this.http.delete(AUTH_API + id);
  }

  deleteAdmin(id:string){
    return this.http.delete(AUTH_API + `admin/${id}`);
  }

  addRoleUser(RolId:string, UserId:string){
    return this.http.post(AUTH_API + 'addRoletoUser',{
        RolId,
        UserId
    }, httpOptions);
  }

}

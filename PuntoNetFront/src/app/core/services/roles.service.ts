import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/roles/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class RolesService {

  constructor(private http: HttpClient) { }

  getRoles():Observable<any[]>{
    return this.http.get<any>(AUTH_API/*'admin'*/);
  }

  getRole(id: number):Observable<any[]>{
    return this.http.get<any>(AUTH_API+id);
  }

  postRole(name: string){
    return this.http.post(AUTH_API , {
      name,
    },httpOptions);
  }

  putRole(id:number, name: string){
     return this.http.put(AUTH_API + id, {
      name,
     }, httpOptions);
  }

  deleteRole(id:string){
    return this.http.delete(AUTH_API + id);
  }

  
}

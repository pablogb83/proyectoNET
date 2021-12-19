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
    return this.http.get<any>(AUTH_API);
  }

  getRole(id: number):Observable<any[]>{
    return this.http.get<any>(AUTH_API+id);
  }
}

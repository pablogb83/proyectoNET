import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/edificios/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class EdificiosService {

  constructor(private http: HttpClient) { }

  getEdificios():Observable<any[]>{
    return this.http.get<any>(AUTH_API);
  }

  getEdificio(id: number):Observable<any[]>{
    return this.http.get<any>(AUTH_API+id);
  }

  postEdificios(nombre: string, direccion: string, telefono: string){
    return this.http.post(AUTH_API , {
      nombre,
      direccion,
      telefono
    },httpOptions);
  }

  putEdificio(id:number, nombre: string, direccion: string, telefono: string){
     return this.http.put(AUTH_API + id, {
      nombre,
      direccion,
      telefono
     }, httpOptions);
  }

  deleteEdificio(id:string){
    return this.http.delete(AUTH_API + id);
  }
}

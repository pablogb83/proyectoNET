import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/accesos/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AccesoService {

  constructor(private http: HttpClient) { }

  getAccesosEdificio(id: string):Observable<any[]>{
    return this.http.get<any>(AUTH_API + 'edificio/' + id);
  }

  getAccesosPuerta(id: string):Observable<any[]>{
    return this.http.get<any>(AUTH_API + 'puertas/' + id);
  }

  getAccesosPersona(id: number):Observable<any[]>{
    return this.http.get<any>(AUTH_API + 'persona/' + id);
  }

  getAccesos():Observable<any[]>{
    return this.http.get<any>(AUTH_API);
  }

  getAcceso(id: string):Observable<any[]>{
    return this.http.get<any>(AUTH_API+id);
  }

  postAcceso(val:any){
    return this.http.post(AUTH_API ,val,httpOptions);
  }

  deleteAcceso(id:string){
    return this.http.delete(AUTH_API + id);
  }

  putAcceso(id:string, val:any){
    return this.http.put(AUTH_API + id, val, httpOptions);
 }
}

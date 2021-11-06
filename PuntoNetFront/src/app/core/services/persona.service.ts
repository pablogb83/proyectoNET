import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/personas/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class PersonaService {

  constructor(private http: HttpClient) { }

  getPersonas():Observable<any[]>{
    return this.http.get<any>(AUTH_API);
  }

  getPersona(id: number):Observable<any[]>{
    return this.http.get<any>(AUTH_API+id);
  }

  postPersona(val:any){
    return this.http.post(AUTH_API ,val,httpOptions);
  }

  putPersona(id:string, val:any){
     return this.http.put(AUTH_API + id, val, httpOptions);
  }

  deletePersona(id:string){
    return this.http.delete(AUTH_API + id);
  }

  altaMasiva(ruta:string):Observable<any>{
    console.log("estoy en el service")
    return this.http.post(AUTH_API + 'altaMasiva/' + ruta , httpOptions);
  }

}

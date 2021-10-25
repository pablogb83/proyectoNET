import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const AUTH_API = '/api/puerta/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class PuertaService {

  constructor(private http: HttpClient) { }

  addPuerta(val:any){
    return this.http.post(AUTH_API , val);
  }

  deletePuerta(id:string){
    return this.http.delete(AUTH_API + id);
  }

  updatePuerta(id:string, val:any){
    return this.http.put(AUTH_API + id,  val);
 }

}

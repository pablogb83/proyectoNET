import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const AUTH_API = '/api/salon/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class SalonService {

  constructor(private http: HttpClient) { }

  addSalon(val:any){
    return this.http.post(AUTH_API , val);
  }

  deleteSalon(id:string){
    return this.http.delete(AUTH_API + id);
  }

  updateSalon(id:string, val:any){
    return this.http.put(AUTH_API + id,  val);
  }
  

}

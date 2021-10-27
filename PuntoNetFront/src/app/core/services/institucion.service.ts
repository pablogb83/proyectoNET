import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/instituciones/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class InstitucionService {

  constructor(private http: HttpClient) { }

  getInstList():Observable<any[]>{
    return this.http.get<any>(AUTH_API);
  }

  addInst(val:any){
    return this.http.post(AUTH_API , val);
  }

  updateInst(id:string, val:any){
     return this.http.put(AUTH_API + id,  val);
  }

  deleteInst(val:any){
    return this.http.delete(AUTH_API + val);
  }

  isActive(id: string){
    if(!id){
      id='undefined'
    }
    return this.http.get(AUTH_API  + 'active/' + id);
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/registro/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class PagoService {

  constructor(private http: HttpClient) { }

 
  pagarCuota(): Observable<any> {
    return this.http.post(AUTH_API,httpOptions);
  }

}

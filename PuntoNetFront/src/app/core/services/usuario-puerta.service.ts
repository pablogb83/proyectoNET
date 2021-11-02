import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const AUTH_API = '/api/usuarioPuerta/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class UsuarioPuertaService {

  constructor(private http: HttpClient) { }

  addUserPuerta(usuarioId:string, puertaId:string){
    return this.http.post(AUTH_API,{
      usuarioId,
      puertaId
    }, httpOptions);
  }
}

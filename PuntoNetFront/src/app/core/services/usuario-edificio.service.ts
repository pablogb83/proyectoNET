import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/usuarioEdificio/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class UsuarioEdificioService {

  constructor(private http: HttpClient) { }

  addUserEdificio(usuarioId:string, edificioId:string){
    return this.http.post(AUTH_API,{
      usuarioId,
      edificioId
    }, httpOptions);
  }

  getUsuariosEdificio(id:number):Observable<any[]>{
    return this.http.get<any>(AUTH_API + id);
  }
  
}

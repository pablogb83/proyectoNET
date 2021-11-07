import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/eventos/';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class EventosService {

  // readonly PhotoUrl = "https://localhost:44396/Photos/";

  constructor(private http: HttpClient) { }

  getEventos():Observable<any[]>{
    return this.http.get<any>(AUTH_API);
  }

  getEvento(id: number):Observable<any[]>{
    return this.http.get<any>(AUTH_API+id);
  }

  postEvento(Nombre: string, Descripcion: string, FechaInicioEvt: Date, FechaFinEvt: Date, PhotoFileName: string){
    return this.http.post(AUTH_API , {
      Nombre,
      Descripcion,
      FechaInicioEvt,
      FechaFinEvt,
      PhotoFileName
    },httpOptions);
  }

  putEvento(Id:number, Nombre: string, Descripcion: string, FechaInicioEvt: string, FechaFinEvt: string, PhotoFileName: string){
     return this.http.put(AUTH_API + Id, {
      Nombre,
      Descripcion,
      FechaInicioEvt,
      FechaFinEvt,
      PhotoFileName
     }, httpOptions);
  }

  deleteEvento(id:string){
    return this.http.delete(AUTH_API + id);
  }

  // UploadPhoto(val:any){
  //   return this.http.post(AUTH_API +'SaveFile',val);
  // }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/noticias/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class NoticiasService {

  constructor(private http: HttpClient) { }

  getNoticias():Observable<any[]>{
    return this.http.get<any>(AUTH_API);
  }

  getNoticiasPublicas(idinstitucion?: string):Observable<any[]>{
    return this.http.get<any>(AUTH_API+`publicas?institucion=${idinstitucion??""}`);
  }
  getUltimasNoticias():Observable<any[]>{
    return this.http.get<any>(AUTH_API + 'ultimas');
  }

  getNoticia(id: number):Observable<any[]>{
    return this.http.get<any>(AUTH_API + id);
  }

  postNoticia(Nombre: string, Descripcion: string, publicadoPor: string, FechaPublicacion: Date, PhotoFileName: string){
    return this.http.post(AUTH_API , {
      Nombre,
      Descripcion,
      publicadoPor,
      FechaPublicacion,
      PhotoFileName
    }, httpOptions);
  }

  putNoticia(Id:number, Nombre: string, Descripcion: string, publicadoPor: string, FechaPublicacion: Date, PhotoFileName: string){
     return this.http.put(AUTH_API + Id, {
      Nombre,
      Descripcion,
      publicadoPor,
      FechaPublicacion,
      PhotoFileName
     }, httpOptions);
  }

  deleteNoticia(id:string){
    return this.http.delete(AUTH_API + id);
  }

}

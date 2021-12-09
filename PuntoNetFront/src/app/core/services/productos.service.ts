import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const AUTH_API = '/api/productos';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  constructor(private http: HttpClient) { }

  getProductos():Observable<any[]>{
    return this.http.get<any>(AUTH_API);
  }

  getProducto(id?:string):Observable<any[]>{
    return this.http.get<any>(AUTH_API+`/${id}`);
  }

  postProducto(val:any){
    return this.http.post(AUTH_API ,val, httpOptions);
  }

  putProducto(id:string, precio:number){
     return this.http.put(AUTH_API + `?plan_id=${id}&precio=${precio}`, httpOptions);
  }

  deleteProducto(id:string){
    return this.http.delete(AUTH_API + `/${id}`, httpOptions);
  }

}

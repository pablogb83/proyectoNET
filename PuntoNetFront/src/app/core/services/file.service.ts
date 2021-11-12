import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const AUTH_API = '/api/files/';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
  providedIn: 'root'
})
export class FileService {

  readonly PhotoUrl = "https://localhost:44396/Files/Photos/";

  constructor(private http: HttpClient) { }

  UploadPhoto(val:any){
    return this.http.post(AUTH_API + 'fotos' ,val);
  }

  Uploadfile(val:any){
    return this.http.post(AUTH_API + 'archivos' ,val);
  }

  UploadfileFace(val:any){
    return this.http.post('/api/usuarios/compareFaces' ,val);
  }
}

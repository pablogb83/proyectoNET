import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from '../core/services/token-storage.service';
import { UsuarioEdificioService } from '../core/services/usuario-edificio.service';

@Component({
  selector: 'app-panel-opciones',
  templateUrl: './panel-opciones.component.html',
  styleUrls: ['./panel-opciones.component.css']
})
export class PanelOpcionesComponent implements OnInit {

  rol:string;
  instActive: boolean;
  idedificio:number;
  idUsuario:string;

  constructor(private tokenService: TokenStorageService, private usuarioEdificio: UsuarioEdificioService,) { }

  ngOnInit(): void {
    this.rol = this.tokenService.getRoleName();
    this.instActive = this.tokenService.getStatus();
    this.idUsuario = this.tokenService.getUserId();
    if(this.rol==='PORTERO' && this.idUsuario){
      this.usuarioEdificio.getEdificioUsuario(this.idUsuario).subscribe(data=>{
          console.log(data);
          this.idedificio = data.id;
      })
    }
  }

}

import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from '../core/services/token-storage.service';

@Component({
  selector: 'app-panel-opciones',
  templateUrl: './panel-opciones.component.html',
  styleUrls: ['./panel-opciones.component.css']
})
export class PanelOpcionesComponent implements OnInit {

  rol:string;
  instActive: boolean;

  constructor(private tokenService: TokenStorageService) { }

  ngOnInit(): void {
    this.rol = this.tokenService.getRoleName();
    this.instActive = this.tokenService.getStatus();
  }

}

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MatTableDataSource, MAT_DIALOG_DATA } from '@angular/material';
import { Router } from '@angular/router';
import { AccesoService } from 'src/app/core/services/acceso.service';
import { FileService } from 'src/app/core/services/file.service';
import { PersonaService } from 'src/app/core/services/persona.service';
import { Persona } from 'src/app/persona/persona-list/persona-list.component';
import Swal from 'sweetalert2';
import { Data } from '../acceso-list/acceso-list.component';

@Component({
  selector: 'app-acceso-add',
  templateUrl: './acceso-add.component.html',
  styleUrls: ['./acceso-add.component.css']
})
export class AccesoAddComponent  {

  idPuerta?:any;
  idPersona?:any;
  PersonasList:any=[];
  PhotoFileName?:any;
  PhotoFilePath?:any;

  constructor(public dialogRef: MatDialogRef<AccesoAddComponent>,@Inject(MAT_DIALOG_DATA) public data: Data, private service:AccesoService, private personaService:PersonaService,private fileService:FileService, public router: Router) {
    this.idPuerta = data.idpuerta;
    console.log(this.idPuerta);
    this.getPersonas();
   }



  agregarAcceso(){
    var val = {
      fechaHora:new Date(),
      puertaId:this.idPuerta,
      personaId:this.idPersona,
      };
      this.service.postAcceso(val).subscribe(res=>{
        this.showSuccessAlert();
      }, err =>{
        this.showErrorAlert();
      });
  }

   getPersonas(): void{
     this.personaService.getPersonas().subscribe(datos=>{
      this.PersonasList =datos;
      console.log(this.PersonasList)
     });
  }

  busqueda(event: any): void{
    console.log('Estoy buuscando', event.target.value);
    this.getPersonasBusqueda(event.target.value);
  }

  getPersonasBusqueda(filter: string): void{
    if(filter===''){
      this.getPersonas();
    }else{
      this.personaService.getPersonasBusqueda(filter).subscribe(data=>{
        this.PersonasList = data;
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  obtenerPersona(persona:any):void{
    console.log(persona);
    this.PhotoFilePath=this.fileService.PhotoUrl + persona.photoFileName;
  }

  redireccionar():void{
    this.router.navigate(['/personas']);
    this.onNoClick();
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Acceso registrado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

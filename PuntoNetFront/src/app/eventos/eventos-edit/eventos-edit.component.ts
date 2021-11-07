import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { EventosService } from 'src/app/core/services/eventos.service';
import { FileService } from 'src/app/core/services/file.service';
import { InstEditComponent } from 'src/app/institucion/inst-edit/inst-edit.component';
import Swal from 'sweetalert2';
import { Evento } from '../eventos-list/eventos-list.component';

@Component({
  selector: 'app-eventos-edit',
  templateUrl: './eventos-edit.component.html',
  styleUrls: ['./eventos-edit.component.css']
})
export class EventosEditComponent implements OnInit {

  id?:string;
  nombre?:string;
  descripcion?:string;
  fechainicio?:string;
  fechafin?:string;
  PhotoFileName?:any;
  PhotoFilePath?:any;

  constructor(public dialogRef: MatDialogRef<EventosEditComponent>, @Inject(MAT_DIALOG_DATA) public data: Evento, private service:EventosService,private fileService:FileService) { 
    console.log(data);
    this.id = data.id;
    this.nombre = data.nombre;
    this.descripcion = data.descripcion;

    let s = new Date(data.fechaInicioEvt)
    this.fechainicio = s.getMonth()+1 + "/" + s.getDate() + "/" + s.getFullYear(); //06/15/2021

    let s2 = new Date(data.fechaFinEvt)
    this.fechafin = s2.getMonth()+1 + "/" + s2.getDate() + "/" + s2.getFullYear(); //06/15/2021

    this.PhotoFileName = data.photoFileName;
    this.PhotoFilePath=this.fileService.PhotoUrl+this.PhotoFileName;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  updateEvento(){
    var id = this.id;
    var idcast: number = +id;
    
    var val = {
              nombre:this.nombre,
              descripcion:this.descripcion,
              fechainicio:this.fechainicio,
              fechafin:this.fechafin,
              PhotoFileName: this.PhotoFileName
            };

    this.service.putEvento(Number(id), val.nombre,val.descripcion, val.fechainicio, val.fechafin, val.PhotoFileName).subscribe(res=>{
    this.showSuccessAlert();
    }, err=>{
      this.showErrorAlert();
    });
  }

  uploadPhoto(event){
    var file=event.target.files[0];
    const formData:FormData=new FormData();
    formData.append('uploadedFile',file,file.name);

    this.fileService.UploadPhoto(formData).subscribe((data)=>{
      this.PhotoFileName=data.toString();
      this.PhotoFilePath=this.fileService.PhotoUrl+this.PhotoFileName;
    })

  }

  showSuccessAlert() {
    Swal.fire('OK', 'Evento actualizado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

  ngOnInit() {
  }


}

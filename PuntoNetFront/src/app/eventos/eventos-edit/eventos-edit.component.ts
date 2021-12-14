import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EventosService } from 'src/app/core/services/eventos.service';
import { FileService } from 'src/app/core/services/file.service';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import Swal from 'sweetalert2';
import { Evento } from '../eventos-list/eventos-list.component';
import moment from 'moment';
import { Time } from '@angular/common';
import { Salon } from 'src/app/salon/salon-list/salon-list.component';
import { EdificiosService } from 'src/app/core/services/edificios.service';


@Component({
  selector: 'app-eventos-edit',
  templateUrl: './eventos-edit.component.html',
  styleUrls: ['./eventos-edit.component.css']
})
export class EventosEditComponent implements OnInit {

  id?:string;
  nombre?:string;
  descripcion?:string;
  fechainicio?:string="";
  fechafin?:string="";
  PhotoFileName?:any;
  PhotoFilePath?:any;
  hora: Time;
  duracion=0;
  salon: Salon;
  salonId: string;
  edificio: any;
  salones: any[] = [];



  constructor(public dialogRef: MatDialogRef<EventosEditComponent>, @Inject(MAT_DIALOG_DATA) public data: Evento, private service:EventosService,private fileService:FileService, private handleError: HandleErrorsService, private edificioService: EdificiosService) { 
    console.log(data);
    this.id = data.id;
    this.nombre = data.nombre;
    this.descripcion = data.descripcion;
    this.fechainicio = data.fechaInicioEvt;//s.getMonth()+1 + "/" + s.getDate() + "/" + s.getFullYear(); //06/15/2021
    this.fechafin = data.fechaFinEvt//s2.getMonth()+1 + "/" + s2.getDate() + "/" + s2.getFullYear(); //06/15/2021
    this.salon = data.salon;
    this.salonId = data.salon.id;
    this.edificio = data.salon.edificio;
    edificioService.getSalonesEdificio(this.edificio.id).subscribe(data=>{
      this.salones = data;
    });
    this.PhotoFilePath=this.fileService.PhotoUrl+this.PhotoFileName;
  }
  
  ngOnInit() {
    
  }
  onNoClick(): void {
    this.dialogRef.close();
  }

  updateEvento(){
    var id = this.id;
    
    var val = {
      nombre:this.nombre,
      descripcion:this.descripcion,
      fechainicio: moment(this.fechainicio).format("YYYY-MM-DD HH:mm:ss"),
      fechafin:moment(this.fechafin).format("YYYY-MM-DD HH:mm:ss"),
      PhotoFileName: this.PhotoFileName
    };
    console.log("El salon seleccionado es: ", this.salon, " DATOS: ", val);
    this.service.putEvento(Number(id), val.nombre,val.descripcion, val.fechainicio, val.fechafin, val.PhotoFileName,Number(this.salonId)).subscribe(res=>{
      this.handleError.showSuccessAlert();
    }, err=>{
      this.handleError.showErrors(err);
    });
  }

  getFechaFin(){
    const fechainicio =  moment(new Date(this.fechainicio),"YYYY-MM-DD").format("YYYY-MM-DD");
    const horainicio =  moment(this.hora,"HH:mm").format("HH:mm");
    const fechahorainicio =  moment(fechainicio + ' ' + horainicio,"YYYY-MM-DD HH:mm:ss");
    const fechafin = moment(fechainicio + ' ' + horainicio,"YYYY-MM-DD HH:mm:ss").add(this.duracion,'hours')
    return {
      fechainicio: fechahorainicio,
      fechafin: fechafin
    }
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



}

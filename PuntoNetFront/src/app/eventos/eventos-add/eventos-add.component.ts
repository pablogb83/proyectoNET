import { Component, OnInit, Inject } from '@angular/core';
import { EventosService } from 'src/app/core/services/eventos.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { FileService } from 'src/app/core/services/file.service';
import { Time } from '@angular/common';
import moment from 'moment';

@Component({
  selector: 'app-eventos-add',
  templateUrl: './eventos-add.component.html',
  styleUrls: ['./eventos-add.component.css']
})
export class EventosAddComponent implements OnInit {

  nombre?:string;
  descripcion?:string;
  duracion=0;
  hora: Time;
  fechainicio = new FormControl(new Date());
  fechafin = new FormControl(new Date());
  PhotoFileName?:any;
  PhotoFilePath?:any;
  fecha = new Date();

  dias: any[] = [
    {
      selected: false,
      value: 1
    },
    {
      selected: false,
      value: 2
    },
    {
      selected: false,
      value: 3
    },
    {
      selected: false,
      value: 4
    },
    { 
      selected: false,
      value: 5
    },
    {
      selected: false,
      value: 6
    },
    {
      selected: false,
      value: 0
    }
  ]

  tipoEvento ="simple";

  onNoClick(): void {
    this.dialogRef.close();
  }

  constructor(public dialogRef: MatDialogRef<EventosAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:EventosService, private fileService:FileService,private fb: FormBuilder) {
   
   }

  ngOnInit() {
   // this.PhotoFilePath=this.service.PhotoUrl+this.PhotoFileName;
  }

  agregarEvento(){
    const diasSeleccionados = this.dias.filter(x=>{
      if(x.selected){
        return x;
      }
    }).map(x=>{
      return x.value;
    });
    console.log("Los dias son: ", diasSeleccionados, " La fecha de inicio es: ", new Date(this.fechainicio.value)," La hora es: ", moment(this.hora).format("HH:mm")  );
    if(this.tipoEvento==="simple"){
      this.service.postEvento(this.nombre,this.descripcion, this.fechainicio.value, this.fechafin.value, this.PhotoFileName).subscribe(res=>{
        this.showSuccessAlert();
      }, err =>{
        console.log(err);
        this.showErrorAlert();
      });
    }
    else{
      this.service.postEventoRecurrente(this.nombre,this.descripcion,this.fechainicio.value, this.fechafin.value,moment(this.hora).format("HH:mm") ,this.duracion,diasSeleccionados).subscribe(data=>{
        this.showSuccessAlert();
      },err=>{
        console.log(err);
        this.showErrorAlert();
      });
    }
   
  }

  uploadPhoto(event){
    var file=event.target.files[0];
    const formData:FormData=new FormData();
    formData.append('uploadedFile',file,file.name);

    this.fileService.UploadPhoto(formData).subscribe((data)=>{
      this.PhotoFileName=data.toString();
      this.PhotoFilePath=this.fileService.PhotoUrl + this.PhotoFileName;
    })

  }

  showSuccessAlert() {
    Swal.fire('OK', 'Evento agregado con exito!', 'success');
  }

  showErrorAlert(msg?) {
    Swal.fire('Error!', msg ? msg : 'Algo salió mal', 'error');
  }

}

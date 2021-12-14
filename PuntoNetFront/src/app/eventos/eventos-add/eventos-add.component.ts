import { Component, OnInit, Inject, HostListener } from '@angular/core';
import { EventosService } from 'src/app/core/services/eventos.service';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';
import { FileService } from 'src/app/core/services/file.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Time } from '@angular/common';
import moment from 'moment';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';

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
  idsalon: number;
  fechafin = new FormControl(new Date());
  PhotoFileName?:any;
  PhotoFilePath?:any;
  fecha = new Date();
  salones: any[] = [];
  edificios: any[];
  idEdificio: number;

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
  habilitarAgregar = false; 

  @HostListener('document:click', ['$event'])
    onDocumentClick(event: MouseEvent) {
    console.log(event);
      this.habilitarAgregar = false;
      this.salones = []
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  constructor(public dialogRef: MatDialogRef<EventosAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:EventosService, private fileService:FileService,private fb: FormBuilder, private edificioService: EdificiosService, private handleError: HandleErrorsService) {
    this.edificioService.getEdificios().subscribe(data=>{
      console.log(data); 
      this.edificios = data;
     })
   }

  ngOnInit() {
   // this.PhotoFilePath=this.service.PhotoUrl+this.PhotoFileName;

  }

  diasSeleccionados(){
    const diasSeleccionados = this.dias.filter(x=>{
      if(x.selected){
        return x;
      }
    }).map(x=>{
      return x.value;
    });
    return diasSeleccionados
  }

  agregarEvento(){
    // const diasSeleccionados = this.dias.filter(x=>{
    //   if(x.selected){
    //     return x;
    //   }
    // }).map(x=>{
    //   return x.value;
    // });
    if(this.tipoEvento==="simple"){
      const fechas = this.getFechaFin();
      this.service.postEvento(this.nombre,this.descripcion, fechas.fechainicio.format("YYYY-MM-DDTHH:mm:ss"), fechas.fechafin.format("YYYY-MM-DDTHH:mm:ss"), this.idsalon).subscribe((res:any)=>{
        this.handleError.showSuccessAlert(res.message);
      }, err =>{
        console.log(err);
        this.handleError.showErrors(err);
      });
    }
    else{
      this.service.postEventoRecurrente(this.nombre,this.descripcion,this.fechainicio.value, this.fechafin.value,moment(this.hora).format("HH:mm") ,this.duracion,this.diasSeleccionados(),this.idsalon).subscribe((data: any)=>{
        this.handleError.showSuccessAlert(data.message)
      },err=>{
        console.log(err);
        this.handleError.showErrors(err);
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

  updateSalones(){
    const fechas = this.getFechaFin();
    console.log(fechas);
    if(this.tipoEvento === "simple"){
      if(fechas && this.idEdificio && this.duracion && this.hora){
        this.service.getSalonesDisponibles(fechas.fechainicio.format("YYYY-MM-DD HH:mm:ss"),fechas.fechafin.format("YYYY-MM-DD HH:mm:ss"),this.idEdificio,this.tipoEvento, this.diasSeleccionados(), this.duracion, moment(this.hora).format("HH:mm") ).subscribe(data=>{
          console.log(data);
          this.habilitarAgregar = true;
          this.salones = data;
        },err=>{
          this.handleError.showErrors(err);
        });
      }else{
          this.handleError.showErrorAlert(['Ingrese todos los datos']);
      }
    }else{
      if(fechas && this.idEdificio && this.duracion && this.hora && this.diasSeleccionados().length){
        this.service.getSalonesDisponibles(this.fechainicio.value, this.fechafin.value,this.idEdificio,this.tipoEvento, this.diasSeleccionados(), this.duracion, moment(this.hora).format("HH:mm") ).subscribe(data=>{
          console.log(data);
          this.habilitarAgregar = true;
          this.salones = data;
        },err=>{
          this.handleError.showErrors(err);
        });
      }else{
        this.handleError.showErrorAlert(['Ingrese todos los datos']);
      }
    }
    
  }

  getFechaFin(){
    const fechainicio =  moment(new Date(this.fechainicio.value),"YYYY-MM-DD").format("YYYY-MM-DD");
    const horainicio =  moment(this.hora,"HH:mm").format("HH:mm");
    const fechahorainicio =  moment(fechainicio + ' ' + horainicio,"YYYY-MM-DD HH:mm:ss");
    const fechafin = moment(fechainicio + ' ' + horainicio,"YYYY-MM-DD HH:mm:ss").add(this.duracion,'hours')
    return {
      fechainicio: fechahorainicio,
      fechafin: fechafin
    }
  }

}
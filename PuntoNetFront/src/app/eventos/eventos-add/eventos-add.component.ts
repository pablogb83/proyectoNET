import { Component, OnInit, Inject } from '@angular/core';
import { EventosService } from 'src/app/core/services/eventos.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';
import { FormControl } from '@angular/forms';
import { FileService } from 'src/app/core/services/file.service';

@Component({
  selector: 'app-eventos-add',
  templateUrl: './eventos-add.component.html',
  styleUrls: ['./eventos-add.component.css']
})
export class EventosAddComponent implements OnInit {

  nombre?:string;
  descripcion?:string;
  fechainicio = new FormControl(new Date());
  fechafin = new FormControl(new Date());
  PhotoFileName?:any;
  PhotoFilePath?:any;

  onNoClick(): void {
    this.dialogRef.close();
  }

  constructor(public dialogRef: MatDialogRef<EventosAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:EventosService, private fileService:FileService) { }

  ngOnInit() {
   // this.PhotoFilePath=this.service.PhotoUrl+this.PhotoFileName;
  }

  agregarEvento(){
    var initDate = new Date (this.fechainicio.value._i.year, 
                             this.fechainicio.value._i.month, 
                             this.fechainicio.value._i.date );

    var endDate = new Date (this.fechafin.value._i.year, 
                            this.fechafin.value._i.month, 
                            this.fechafin.value._i.date );
  //  console.log(initDate);
    var val = {
              nombre:this.nombre,
              descripcion:this.descripcion,
              fechainicio: initDate, 
              fechafin: endDate,
              PhotoFileName:this.PhotoFileName
              };
    this.service.postEvento(val.nombre,val.descripcion, val.fechainicio, val.fechafin, val.PhotoFileName).subscribe(res=>{
      this.showSuccessAlert();
    }, err =>{
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
    Swal.fire('OK', 'Evento agregado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

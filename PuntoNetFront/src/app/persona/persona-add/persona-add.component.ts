import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { FileService } from 'src/app/core/services/file.service';
import { PersonaService } from 'src/app/core/services/persona.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-persona-add',
  templateUrl: './persona-add.component.html',
  styleUrls: ['./persona-add.component.css']
})
export class PersonaAddComponent implements OnInit {

  nombres?:string;
  apellidos?:string;
  telefono?:string;
  email?:string;
  tipo_doc?: number;
  nro_doc?: string
  PhotoFileName?:any;
  PhotoFilePath?:any;

  constructor(public dialogRef: MatDialogRef<PersonaAddComponent>, private service:PersonaService, private fileService:FileService) { }

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  agregarPersona(){
    var val = {
              nombres:this.nombres,
              apellidos:this.apellidos,
              telefono:this.telefono,
              email:this.email,
              tipo_doc:this.tipo_doc,
              nro_doc:this.nro_doc,
              PhotoFileName:this.PhotoFileName
              };
    this.service.postPersona(val).subscribe(res=>{
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
    Swal.fire('OK', 'Persona agregada con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog/dialog-ref';
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
  tipo_doc?: string;
  nro_doc?: string
  PhotoFileName?:any;
  PhotoFilePath?:any;
  file: any;
  imagePath: string;
  imgURL: any;

  constructor(public dialogRef: MatDialogRef<PersonaAddComponent>, private service:PersonaService, private fileService:FileService) { }

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  agregarPersona(){
    const formData:FormData=new FormData();
    formData.append('uploadedFile',this.file,this.file.name);
    formData.append("nombres",this.nombres);
    formData.append("apellidos",this.apellidos);
    formData.append("telefono",this.telefono);
    formData.append("email",this.email);
    formData.append("tipo_doc",this.tipo_doc);
    formData.append("nro_doc",this.nro_doc);
    this.service.postPersona(formData).subscribe(res=>{
      this.showSuccessAlert();
    }, err =>{
      this.showErrorAlert();
    });
  }

  uploadPhoto(event){
    const files = event.target.files;
    this.file=event.target.files[0];
    if (files.length === 0)
      return;

    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }

    var reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
      this.imgURL = reader.result; 
    }
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Persona agregada con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

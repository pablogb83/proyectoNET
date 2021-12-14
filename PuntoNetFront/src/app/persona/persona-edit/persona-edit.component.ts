import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FileService } from 'src/app/core/services/file.service';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { PersonaService } from 'src/app/core/services/persona.service';
import Swal from 'sweetalert2';
import { Persona } from '../persona-list/persona-list.component';

@Component({
  selector: 'app-persona-edit',
  templateUrl: './persona-edit.component.html',
  styleUrls: ['./persona-edit.component.css']
})
export class PersonaEditComponent implements OnInit {

  id?:string;
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

  constructor(public dialogRef: MatDialogRef<PersonaEditComponent>, @Inject(MAT_DIALOG_DATA) public data: Persona, private service:PersonaService,private fileService:FileService, private handleError: HandleErrorsService) { 
    this.id=data.id;
    this.nombres=data.nombres;
    this.apellidos=data.apellidos;
    this.telefono=data.telefono;
    this.email=data.email;
    this.tipo_doc=data.tipo_doc;
    this.nro_doc=data.nro_doc;
    this.PhotoFileName=data.photoFileName;
    this.PhotoFilePath=this.fileService.PhotoUrl+this.PhotoFileName;
  }

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  updatePersona(){
    const formData:FormData=new FormData();
    if(this.file){
      formData.append('uploadedFile',this.file,this.file.name);
    }
    formData.append("nombres",this.nombres);
    formData.append("apellidos",this.apellidos);
    formData.append("telefono",this.telefono);
    formData.append("email",this.email);
    formData.append("tipo_doc",this.tipo_doc);
    formData.append("nro_doc",this.nro_doc);
    console.log(formData);
    /*this.fileService.UploadPhoto(formData).subscribe((data)=>{
      this.PhotoFileName = data.toString();
      var val = {
        nombres:this.nombres,
        apellidos:this.apellidos,
        telefono:this.telefono,
        email:this.email,
        tipo_doc: this.tipo_doc,
        nro_doc: this.nro_doc,
        PhotoFileName:this.PhotoFileName
      };
    }); */
    var id = this.id;
    this.service.putPersona(id, formData).subscribe(res=>{
      this.handleError.showSuccessAlert();
    }, err=>{
      this.handleError.showErrors(err);
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

}

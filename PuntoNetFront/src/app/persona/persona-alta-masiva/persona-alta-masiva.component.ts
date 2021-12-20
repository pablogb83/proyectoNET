import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FileService } from 'src/app/core/services/file.service';
import { PersonaService } from 'src/app/core/services/persona.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-persona-alta-masiva',
  templateUrl: './persona-alta-masiva.component.html',
  styleUrls: ['./persona-alta-masiva.component.css']
})
export class PersonaAltaMasivaComponent implements OnInit {

  PhotoFileName?:any;
  PhotoFilePath?:any;

  constructor(public dialogRef: MatDialogRef<PersonaAltaMasivaComponent>,private fileService:FileService, private service:PersonaService) { }

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  uploadFile(event){
    var file=event.target.files[0];
    const formData:FormData=new FormData();
    formData.append('uploadedFile',file,file.name);

    this.fileService.Uploadfile(formData).subscribe((data)=>{
      this.PhotoFileName=data.toString();
      this.PhotoFilePath=this.fileService.PhotoUrl+this.PhotoFileName;
    })
  }

  aceptar():void{
    console.log(this.PhotoFileName);
    this.service.altaMasiva(this.PhotoFileName).subscribe(data=>{
      this.showSuccessAlert();
    },err=>{
      console.log(err.error.message)
      this.showErrorAlert(err.error.message);
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Archivo cargado con exito!', 'success');
  }

  showErrorAlert(msg: string) {
    Swal.fire('Error!',msg, 'error');
  }

}

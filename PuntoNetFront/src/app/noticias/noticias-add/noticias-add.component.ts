import { Component, OnInit, Inject } from '@angular/core';
import { NoticiasService } from 'src/app/core/services/noticias.service';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';
import { FormControl } from '@angular/forms';
import { FileService } from 'src/app/core/services/file.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
  selector: 'app-noticias-add',
  templateUrl: './noticias-add.component.html',
  styleUrls: ['./noticias-add.component.css']
})
export class NoticiasAddComponent implements OnInit {


  nombre?:string;
  descripcion?:string;
  publicadopor?:string;
  fechapub = new FormControl();
  PhotoFileName?:any;
  PhotoFilePath?:any;
  // fecha = new Date();

  onNoClick(): void {
    this.dialogRef.close();
  }

  constructor(public dialogRef: MatDialogRef<NoticiasAddComponent>, 
              @Inject(MAT_DIALOG_DATA) public data: DialogData, 
              private service:NoticiasService, 
              private Usrservice: TokenStorageService, 
              private fileService:FileService) { }

  ngOnInit() {
   // this.PhotoFilePath=this.service.PhotoUrl+this.PhotoFileName;
  }

  getPublicador(){

  return this.Usrservice.getUserName();
  }

  agregarNoticia(){
    if(!this.nombre || !this.descripcion){
      return this.showErrorAlert("Complete todos los datos necesarios")
    }
      var fechapub = new Date ();

    
  //  console.log(initDate);
    var val = {
              nombre: this.nombre,
              descripcion: this.descripcion,
              publicadopor: this.getPublicador(),
              fechapub: fechapub,
              PhotoFileName:this.PhotoFileName
              };
              console.log(val);
    this.service.postNoticia(val.nombre,val.descripcion, val.publicadopor, val.fechapub, val.PhotoFileName).subscribe(res=>{
      this.showSuccessAlert();
    }, err =>{
      console.log(err);
      this.showErrorAlert();
    });
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
    Swal.fire('OK', 'Noticia agregada con exito!', 'success');
  }

  showErrorAlert(msg?) {
    Swal.fire('Error!', msg ? msg : 'Algo salió mal', 'error');
  }

}

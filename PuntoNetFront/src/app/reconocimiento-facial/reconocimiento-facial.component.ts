import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { FileService } from '../core/services/file.service';

@Component({
  selector: 'app-reconocimiento-facial',
  templateUrl: './reconocimiento-facial.component.html',
  styleUrls: ['./reconocimiento-facial.component.css']
})
export class ReconocimientoFacialComponent implements OnInit {

  FileName: string ="";

  FilePath: string ="";
  constructor(private fileService: FileService) { }

  ngOnInit() {
  }

  uploadFile(event){
    var file=event.target.files[0];
    const formData:FormData=new FormData();
    formData.append('uploadedFile',file,file.name);

    this.fileService.Uploadfile(formData).subscribe((data: any)=>{
      console.log(data);
        this.showSuccessAlert(data.persona.name);
    },err=>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert(persona: string) {
    Swal.fire('OK', 'La persona de la imagen ingresada se encuentra en el sistema y es el usuario: ' + persona, 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'La persona no se encontro en el sistema', 'error');
  }
}

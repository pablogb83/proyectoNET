import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ok } from 'assert';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { UsuarioEdificioService } from 'src/app/core/services/usuario-edificio.service';
import Swal from 'sweetalert2';

import { DialogData } from '../user-list/user-list.component';


@Component({
  selector: 'app-user-edificio',
  templateUrl: './user-edificio.component.html',
  styleUrls: ['./user-edificio.component.css']
})
export class UserEdificioComponent implements OnInit {

  user: any;
  edificios: any=[];
  selectedValue: string;

  constructor(public dialogRef: MatDialogRef<UserEdificioComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private edificioService:EdificiosService, private service:UsuarioEdificioService) {
    this.user = data.user;
    this.edificioService.getEdificios().subscribe(data=>{
      this.edificios = data
    });
   }

   onNoClick(): void {
    this.dialogRef.close();
  }

  asignarUsuario(edificioId:any): void{
    console.log('el id del edificio es: ' + edificioId);
    console.log('el id del usuario es: ' + this.user.id);
    this.service.addUserEdificio(this.user.id, edificioId).subscribe(res=>{  
      console.log("hola loco" + res);
      this.showSuccessAlert();
    }, err =>{
      console.log("hola loco ", err);
      this.showErrorAlert(err.error);
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Usuario asignado con exito!', 'success');
  }

  showErrorAlert(error:string) {
    Swal.fire('Error!', error, 'error');
  }

  ngOnInit() {
  }

}

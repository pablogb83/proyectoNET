import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UsuariosService } from 'src/app/core/services/usuarios.service';
import { EdificiosAddComponent } from 'src/app/edificios/edificios-add/edificios-add.component';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-users-add',
  templateUrl: './users-add.component.html',
  styleUrls: ['./users-add.component.css']
})
export class UsersAddComponent implements OnInit {

  
  email?:string;
  passwordPlano?:string;

  onNoClick(): void {
    this.dialogRef.close();
  }


  constructor(public dialogRef: MatDialogRef<EdificiosAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:UsuariosService) { }

  ngOnInit() {
  }

  agregarUsuario(){
    var val = {
      email:this.email,
      password:this.passwordPlano
    };
    this.service.postUsuario(val.email,val.password).subscribe(res=>{
      this.showSuccessAlert();
    }, err =>{
      if (err instanceof HttpErrorResponse) {
        const errorMessages = new Array<{ propName: string; errors: string }>();
        console.log(errorMessages);
      }

      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Usuario agregado con exito!', 'success');
  }

  showErrorAlert() {

    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }


}

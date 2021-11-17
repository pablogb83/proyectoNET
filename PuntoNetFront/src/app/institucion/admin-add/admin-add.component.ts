import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UsuariosService } from 'src/app/core/services/usuarios.service';
import { EdificiosAddComponent } from 'src/app/edificios/edificios-add/edificios-add.component';
import { DialogData } from '../institucion-list/institucion-list.component';
import Swal from 'sweetalert2';
import { RolesService } from 'src/app/core/services/roles.service';

@Component({
  selector: 'app-admin-add',
  templateUrl: './admin-add.component.html',
  styleUrls: ['./admin-add.component.css']
})
export class AdminAddComponent implements OnInit {

 
  email?:string;
  passwordPlano?:string;
  institucion?: string;
  administrador?: any;

  onNoClick(): void {
    this.dialogRef.close();
  }


  constructor(public dialogRef: MatDialogRef<EdificiosAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:UsuariosService, private roleService:RolesService) {
    if(data){
      this.institucion = data.id
    }
  }

  ngOnInit() {
  }

  agregarAdministrador(){
    var val = {
      email:this.email,
      password:this.passwordPlano
    };
    if(this.institucion){
      this.service.postAdmin(val.email,val.password,this.institucion).subscribe(res=>{
        this.showSuccessAlert();
      }, err =>{
        this.showErrorAlert();
      });
    }
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Administrador agregado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }


}

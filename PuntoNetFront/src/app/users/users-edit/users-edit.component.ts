import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { UsuariosService } from 'src/app/core/services/usuarios.service';
import { InstEditComponent } from 'src/app/institucion/inst-edit/inst-edit.component';
import Swal from 'sweetalert2';
import { DialogDataUser } from '../user-list/user-list.component';

@Component({
  selector: 'app-users-edit',
  templateUrl: './users-edit.component.html',
  styleUrls: ['./users-edit.component.css']
})
export class UsersEditComponent implements OnInit {

  id?:number;
  email?:string;
  passwordPlano?:string;

  constructor(public dialogRef: MatDialogRef<InstEditComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogDataUser, private service:UsuariosService) { 
    console.log(data);
    this.id = data.id;
    this.email = data.email;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  updateUsuario(){
    var id = this.id;
    //var idcast: number = +id;
    
    var val = {
      email:this.email,
      passwordPlano:this.passwordPlano
    };
    this.service.putUsuario(id, val.email,val.passwordPlano).subscribe(res=>{
    this.showSuccessAlert();
    }, err=>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Institucion actualizada con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

  ngOnInit() {
  }
}

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
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
  verifpass?:string;
  institucion?: string;
  habilitarBoton: boolean = false;

  onNoClick(): void {
    this.dialogRef.close();
  }

  constructor(public dialogRef: MatDialogRef<EdificiosAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:UsuariosService, private handleError: HandleErrorsService) {
    if(data){
      this.institucion = data.id
    }
  }

  checkpass(){
    if(this.passwordPlano){
      if(this.passwordPlano != this.verifpass){
        this.passwordDitinto();
        this.habilitarBoton = false;
      }else{
        this.habilitarBoton = true;
      }
    }
  }

  ngOnInit() {
  }

  agregarUsuario(){
    var val = {
      email:this.email,
      password:this.passwordPlano
    };
    if(this.institucion){
      this.service.postAdmin(val.email,val.password,this.institucion).subscribe((res:any)=>{
        this.handleError.showSuccessAlert(res.message)
      }, err =>{
        this.handleError.showErrors(err);
      });
    }
    else{
      this.service.postUsuario(val.email,val.password).subscribe((res: any)=>{
        this.handleError.showSuccessAlert(res.message)

      }, err =>{
        this.handleError.showErrors(err);
      });
    }
  }

  passwordDitinto(){
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: 'No coincide el password!',
    })
  }

}

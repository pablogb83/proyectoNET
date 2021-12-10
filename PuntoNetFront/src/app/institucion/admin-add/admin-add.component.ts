import { Component, Inject, OnInit } from '@angular/core';
import { UsuariosService } from 'src/app/core/services/usuarios.service';
import { DialogData, InstitucionListComponent } from '../institucion-list/institucion-list.component';
import Swal from 'sweetalert2';
import { RolesService } from 'src/app/core/services/roles.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';

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


  constructor(private dialogRef: MatDialogRef<InstitucionListComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:UsuariosService, private handleError: HandleErrorsService) {
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
      this.service.postAdmin(val.email,val.password,this.institucion).subscribe((res:any)=>{
        this.handleError.showSuccessAlert(res.message)
      }, err =>{
        this.handleError.showErrors(err);
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

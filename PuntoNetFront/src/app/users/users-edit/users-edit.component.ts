import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
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

  constructor(public dialogRef: MatDialogRef<InstEditComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogDataUser, private service:UsuariosService, private handleError: HandleErrorsService, private tokenService: TokenStorageService) { 
    console.log(data);
    this.id = data.id;
    this.email = data.email;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  updateUsuario(){
    const role = this.tokenService.getRoleName();
    var id = this.id;
    var val = {
      email:this.email,
      passwordPlano:this.passwordPlano
    };
    if(role==="ADMIN"){
      this.service.putUsuario(id, val.email,val.passwordPlano).subscribe(res=>{
        this.handleError.showSuccessAlert();
      }, err=>{
        this.handleError.showErrors(err);
      });
    }
    else if(role==="SUPERADMIN"){
      this.service.putAdmin(id, val.email,val.passwordPlano).subscribe(res=>{
        this.handleError.showSuccessAlert();
      }, err=>{
        this.handleError.showErrors(err);
      });
    }

    
  }

  ngOnInit() {
  }
}

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RolesService } from 'src/app/core/services/roles.service';
import { UsuariosService } from 'src/app/core/services/usuarios.service';
import Swal from 'sweetalert2';
import { DialogData } from '../user-list/user-list.component';

@Component({
  selector: 'app-user-role',
  templateUrl: './user-role.component.html',
  styleUrls: ['./user-role.component.css']
})
export class UserRoleComponent implements OnInit {

  user: any;
  roles: any=[];
  selectedValue: string;

  constructor(public dialogRef: MatDialogRef<UserRoleComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private rolservice:RolesService, private userService: UsuariosService) {
    this.user = data.user;
    this.rolservice.getRoles().subscribe(data=>{
      console.log(data);
      var roleFilter = data.filter(function (x){
        return x.name === 'PORTERO' || x.name === 'GESTOR';
      });
      this.roles = roleFilter
    });
   }

  onNoClick(): void {
    this.dialogRef.close();
  }

  asignarRol(rolId:any): void{
    console.log('el id del rol es: ' + rolId);
    console.log('el id del usuario es: ' + this.user.id);
    this.userService.addRoleUser(rolId,this.user.id).subscribe(res=>{
      this.showSuccessAlert();
    }, err =>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Rol agregado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

  ngOnInit() {
  }

}

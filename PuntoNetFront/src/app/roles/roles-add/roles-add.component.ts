import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { RolesService } from 'src/app/core/services/roles.service';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-roles-add',
  templateUrl: './roles-add.component.html',
  styleUrls: ['./roles-add.component.css']
})
export class RolesAddComponent implements OnInit {

  nombre?:string;

  onNoClick(): void {
    this.dialogRef.close();
  }


  constructor(public dialogRef: MatDialogRef<RolesAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:RolesService) { }

  ngOnInit() {
  }

  agregarRole(){
    var val = {
      nombre:this.nombre,
    };
    this.service.postRole(val.nombre).subscribe(res=>{
      this.showSuccessAlert();
    }, err =>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Role agregado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

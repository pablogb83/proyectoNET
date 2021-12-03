import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RolesService } from 'src/app/core/services/roles.service';
import { InstEditComponent } from 'src/app/institucion/inst-edit/inst-edit.component';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-roles-edit',
  templateUrl: './roles-edit.component.html',
  styleUrls: ['./roles-edit.component.css']
})
export class RolesEditComponent implements OnInit {

 
  id?:string;
  nombre?:string;
  direccion?:string;
  telefono?:string;

  constructor(public dialogRef: MatDialogRef<InstEditComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:RolesService) { 
    console.log(data);
    this.id = data.id;
    this.nombre = data.nombre;
    this.direccion = data.direccion;
    this.telefono = data.telefono;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  updateEdificio(){
    var id = this.id;
    //var idcast: number = +id;
    
    var val = {
      nombre:this.nombre,
      telefono:this.telefono
    };

    this.service.putRole(Number(id), val.nombre).subscribe(res=>{
    this.showSuccessAlert();
    }, err=>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Role actualizado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

  ngOnInit() {
  }

}

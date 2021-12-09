import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { InstitucionService } from 'src/app/core/services/institucion.service';

import Swal from 'sweetalert2';
import { DialogData } from '../institucion-list/institucion-list.component';

@Component({
  selector: 'app-inst-edit',
  templateUrl: './inst-edit.component.html',
  styleUrls: ['./inst-edit.component.css']
})
export class InstEditComponent implements OnInit {

  id?:string;
  nombre?:string;
  direccion?:string;
  telefono?:string;

  constructor(public dialogRef: MatDialogRef<InstEditComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:InstitucionService) {
    this.id = data.id;
    this.nombre = data.nombre;
    this.direccion = data.direccion;
    this.telefono = data.telefono;
   }

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
  }

  updateInstitucion(){
    var id = this.id;
    //var idcast: number = +id;
    
    var val = {
              name:this.nombre,
              direccion:this.direccion,
              telefono:this.telefono};

    this.service.updateInst(String(id), val).subscribe(res=>{
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

}

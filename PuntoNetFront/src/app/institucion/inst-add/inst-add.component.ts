import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { InstitucionService } from 'src/app/core/services/institucion.service';

import Swal from 'sweetalert2';
import { DialogData } from '../institucion-list/institucion-list.component';

@Component({
  selector: 'app-inst-add',
  templateUrl: './inst-add.component.html',
  styleUrls: ['./inst-add.component.css']
})
export class InstAddComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<InstAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:InstitucionService) {}

  nombre?:string;
  direccion?:string;
  telefono?:string;

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
  }

  agregarInstitucion(){
    var val = {
              name:this.nombre,
              direccion:this.direccion,
              telefono:this.telefono
              };
    this.service.addInst(val).subscribe(res=>{
      this.showSuccessAlert();
    }, err =>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Institucion agregada con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

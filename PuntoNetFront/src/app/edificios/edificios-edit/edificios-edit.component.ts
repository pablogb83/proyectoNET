import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { InstEditComponent } from 'src/app/institucion/inst-edit/inst-edit.component';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edificios-edit',
  templateUrl: './edificios-edit.component.html',
  styleUrls: ['./edificios-edit.component.css']
})
export class EdificiosEditComponent implements OnInit {

  id?:string;
  nombre?:string;
  direccion?:string;
  telefono?:string;

  constructor(public dialogRef: MatDialogRef<InstEditComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:EdificiosService) { 
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
              direccion:this.direccion,
              telefono:this.telefono};

    this.service.putEdificio(Number(id), val.nombre,val.direccion,val.telefono).subscribe(res=>{
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

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edificios-add',
  templateUrl: './edificios-add.component.html',
  styleUrls: ['./edificios-add.component.css']
})
export class EdificiosAddComponent implements OnInit {

  nombre?:string;
  direccion?:string;
  telefono?:string;

  onNoClick(): void {
    this.dialogRef.close();
  }


  constructor(public dialogRef: MatDialogRef<EdificiosAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:EdificiosService) { }

  ngOnInit() {
  }

  agregarEdificio(){
    var val = {
              nombre:this.nombre,
              direccion:this.direccion,
              telefono:this.telefono
              };
    this.service.postEdificios(val.nombre,val.direccion,val.telefono).subscribe(res=>{
      this.showSuccessAlert();
    }, err =>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Edificio agregado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { PuertaService } from 'src/app/core/services/puerta.service';
import Swal from 'sweetalert2';
import { EdificioIdData } from '../puerta-list/puerta-list.component';

@Component({
  selector: 'app-puerta-add',
  templateUrl: './puerta-add.component.html',
  styleUrls: ['./puerta-add.component.css']
})
export class PuertaAddComponent implements OnInit {

  denominacion?:string;
  idEdificio?:number;

  constructor(public dialogRef: MatDialogRef<PuertaAddComponent>,@Inject(MAT_DIALOG_DATA) public data: EdificioIdData, private service:PuertaService) {
    this.idEdificio = data.idedificio;
   }

  ngOnInit() {
  }

  agregarPuerta(){
    var val = {
      denominacion:this.denominacion,
      idEdificio:this.idEdificio
      };
      this.service.addPuerta(val).subscribe(res=>{
        this.showSuccessAlert();
      }, err =>{
        this.showErrorAlert();
      });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Puerta agregada con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

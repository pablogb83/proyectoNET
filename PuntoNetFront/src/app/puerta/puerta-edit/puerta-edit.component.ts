import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PuertaService } from 'src/app/core/services/puerta.service';
import Swal from 'sweetalert2';
import { Puerta } from '../puerta-list/puerta-list.component';

@Component({
  selector: 'app-puerta-edit',
  templateUrl: './puerta-edit.component.html',
  styleUrls: ['./puerta-edit.component.css']
})
export class PuertaEditComponent implements OnInit {

  id?:string;
  denominacion?:string;

  constructor(public dialogRef: MatDialogRef<PuertaEditComponent>, @Inject(MAT_DIALOG_DATA) public data: Puerta,private service:PuertaService) { 
    this.id = data.id;
    this.denominacion = data.denominacion;
  }

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  updatePuerta(){
    var id = this.id;

    var val = {
      denominacion:this.denominacion,
    };

    this.service.updatePuerta(id, val).subscribe(res=>{
    this.showSuccessAlert();
    }, err=>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Puerta actualizada con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SalonService } from 'src/app/core/services/salon.service';
import Swal from 'sweetalert2';
import { Salon } from '../salon-list/salon-list.component';

@Component({
  selector: 'app-salon-edit',
  templateUrl: './salon-edit.component.html',
  styleUrls: ['./salon-edit.component.css']
})
export class SalonEditComponent implements OnInit {

  id?:string;
  denominacion?:string;
  numero?:string;

  constructor(public dialogRef: MatDialogRef<SalonEditComponent>, @Inject(MAT_DIALOG_DATA) public data: Salon,private service:SalonService) {
      this.id = data.id;
      this.denominacion = data.denominacion;
      this.numero = data.numero;
   }

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  updateSalon(){
    var id = this.id;

    var val = {
      denominacion:this.denominacion,
      numero:this.numero
    };

    this.service.updateSalon(id, val).subscribe(res=>{
    this.showSuccessAlert();
    }, err=>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Salon actualizado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

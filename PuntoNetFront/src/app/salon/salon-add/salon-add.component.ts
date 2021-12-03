import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SalonService } from 'src/app/core/services/salon.service';
import Swal from 'sweetalert2';
import { EdificioIdData } from '../salon-list/salon-list.component';

@Component({
  selector: 'app-salon-add',
  templateUrl: './salon-add.component.html',
  styleUrls: ['./salon-add.component.css']
})
export class SalonAddComponent implements OnInit {

  denominacion?:string;
  numero?:number;
  idEdificio?:number;

  constructor(public dialogRef: MatDialogRef<SalonAddComponent>,@Inject(MAT_DIALOG_DATA) public data: EdificioIdData, private service:SalonService) {
    this.idEdificio = data.idedificio;
   }

  ngOnInit() {
  }

  agregarSalon(){
    var val = {
      denominacion:this.denominacion,
      numero:this.numero,
      idEdificio:this.idEdificio
      };
      this.service.addSalon(val).subscribe(res=>{
        this.showSuccessAlert();
      }, err =>{
        this.showErrorAlert();
      });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Salon agregado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }
}

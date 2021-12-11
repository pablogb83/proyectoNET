import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { InstitucionService } from 'src/app/core/services/institucion.service';
import { ProductoService } from 'src/app/core/services/productos.service';

import Swal from 'sweetalert2';
import { DialogData } from '../institucion-list/institucion-list.component';

@Component({
  selector: 'app-inst-add',
  templateUrl: './inst-add.component.html',
  styleUrls: ['./inst-add.component.css']
})
export class InstAddComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<InstAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:InstitucionService, private productService: ProductoService, private handleError: HandleErrorsService) {
    this.productService.getProductos().subscribe(data=>{
      console.log(data);
      this.productos = data;
   }); 
  }

  nombre?:string;
  direccion?:string;
  telefono?:string;
  plan_id?: string;
  productos?: any[];
  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {

  }

  agregarInstitucion(){
    var val = {
      name:this.nombre,
      direccion:this.direccion,
      telefono:this.telefono,
      planid:this.plan_id
    };
    this.service.addInst(val).subscribe(res=>{
      this.handleError.showSuccessAlert();
    }, err =>{
      this.handleError.showErrors(err);
    });
  }

}

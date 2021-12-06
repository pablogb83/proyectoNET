import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { ProductoService } from 'src/app/core/services/productos.service';

@Component({
  selector: 'app-productos-add',
  templateUrl: './productos-add.component.html',
  styleUrls: ['./productos-add.component.css']
})
export class ProductosAddComponent implements OnInit {

  nombre: string = "";
  descripcion: string = "";
  precio: number = 0;

  constructor(private service: ProductoService, public dialogRef: MatDialogRef<ProductosAddComponent>, private handleError: HandleErrorsService) {

  }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  agregarProducto(){
    const val={
      nombre: this.nombre,
      descripcion: this.descripcion,
      precio: this.precio
    }
    this.service.postProducto(val).subscribe((data: any)=>{
      this.handleError.showSuccessAlert(data.message);
    },err=>{
      this.handleError.showErrors(err);
    });
  }

}

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { ProductoService } from 'src/app/core/services/productos.service';

@Component({
  selector: 'app-productos-edit-price',
  templateUrl: './productos-edit-price.component.html',
  styleUrls: ['./productos-edit-price.component.css']
})
export class ProductosEditPriceComponent implements OnInit {

  precio: number = 0;

  constructor(public dialogRef: MatDialogRef<ProductosEditPriceComponent>,private service: ProductoService, @Inject(MAT_DIALOG_DATA) public data: any,private handleError: HandleErrorsService) {
    console.log("El prodcto es: ",data);
    this.precio = data.price;
   }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  editarPrecio(){
    this.service.putProducto(this.data.id,this.precio).subscribe((data:any)=>{
      this.handleError.showSuccessAlert(data.message)
    },err=>{
      this.handleError.showErrors(err);
    });
  }

}

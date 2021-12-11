import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { ProductoService } from 'src/app/core/services/productos.service';
import { ProductosAddComponent } from '../productos-add/productos-add.component';
import { ProductosEditPriceComponent } from '../productos-edit-price/productos-edit-price.component';

@Component({
  selector: 'app-productos-list',
  templateUrl: './productos-list.component.html',
  styleUrls: ['./productos-list.component.css']
})
export class ProductosListComponent implements OnInit, AfterViewInit {

  ProductosList:any=[];

  displayedColumns: string[] = ['id','name', 'description', 'status', 'price','acciones'];

  inst:any;

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  constructor(private service: ProductoService,public dialog: MatDialog, private handleError: HandleErrorsService) { 
    this.getProductos();
  }

  getProductos(){
    this.service.getProductos().subscribe(data=>{
      console.log(data);
      this.ProductosList = new MatTableDataSource<Producto>(data);
      this.ProductosList.paginator = this.paginator;
    });
  }

  ngAfterViewInit() {
    this.ProductosList.paginator = this.paginator;
  }

  ngOnInit(): void {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ProductosAddComponent, {
      width: '500px',
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getProductos();
    });
  }

  openDialogUpdate(element: any): void {
    const dialogRef = this.dialog.open(ProductosEditPriceComponent, {
      width: '500px',
      data:element
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getProductos();
    });
  }

  deleteProduct(element){
    this.service.deleteProducto(element.id).subscribe((data: any)=>{
      this.handleError.showSuccessAlert(data.message);
      this.getProductos();
    },err=>{
      this.handleError.showErrors(err);
    })
  }

}
export interface Producto {
  id: string;
  name: string;
  description: string;
  status: string;
  price: number;
}


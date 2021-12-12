import { AfterViewInit, Component, OnInit } from '@angular/core';
import { InstitucionService } from '../core/services/institucion.service';
import { PagoService } from '../core/services/pago.service';
import { ProductoService } from '../core/services/productos.service';

@Component({
  selector: 'app-pago',
  templateUrl: './pago.component.html',
  styleUrls: ['./pago.component.css']
})
export class PagoComponent implements AfterViewInit {

  Producto: any
  institucion: any;

  constructor(private pagoService: PagoService, private productoService: ProductoService,private institucionService: InstitucionService) { 

  }
  ngAfterViewInit(): void {
    this.institucionService.getInstitucion().subscribe(data=>{
      this.institucion = data
      this.productoService.getProducto().subscribe(data=>{
        this.Producto = data
      })
    })
  }


  pagar(){

    this.pagoService.pagarCuota().subscribe(data=>{
      console.log(data);
      const url = data.link;
      window.location.href = url;
    });
  }

}
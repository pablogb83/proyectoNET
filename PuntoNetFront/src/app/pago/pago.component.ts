import { Component, OnInit } from '@angular/core';
import { PagoService } from '../core/services/pago.service';

@Component({
  selector: 'app-pago',
  templateUrl: './pago.component.html',
  styleUrls: ['./pago.component.css']
})
export class PagoComponent implements OnInit {

  constructor(private pagoService: PagoService) { }

  ngOnInit() {
  }

  pagar(){

    this.pagoService.pagarCuota().subscribe(data=>{
      console.log(data);
      const url = data.link;
      window.location.href = url;
    });
  }

}
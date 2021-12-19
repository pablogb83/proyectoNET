import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { InstitucionService } from 'src/app/core/services/institucion.service';
import { FacturacionDetalleComponent } from '../facturacion-detalle/facturacion-detalle.component';

@Component({
  selector: 'app-facturacion-list',
  templateUrl: './facturacion-list.component.html',
  styleUrls: ['./facturacion-list.component.css']
})
export class FacturacionListComponent implements OnInit {
  
  FacturacionList:any=[];

  displayedColumns: string[] = ['id','status', 'pago', 'email', 'time','acciones'];

  fechainicio: Date = new Date("2018-01-01T01:01:01");
  fechafin: Date = new Date();


  inst?:any;

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  constructor(private service: InstitucionService,public dialog: MatDialog, private handleError: HandleErrorsService, private route: ActivatedRoute, private router: Router) { 
    this.route.queryParams.subscribe(params=>{
      if(params.id){
        this.service.getInstitucionById(params.id).subscribe((data:any)=>{
          this.inst = data;
          console.log(this.inst);
          this.getFacturacion(data.id);
        },err=>{
          router.navigate(["error"]);
        });
      }
      else{
        this.getFacturacion();
      }
      
    });
  }

  getFacturacion(id?){
    this.service.getFacturacion(this.fechainicio.toISOString(),this.fechafin.toISOString(),id).subscribe((data: any)=>{
      this.FacturacionList = new MatTableDataSource<Factura>(data);
      this.FacturacionList.paginator = this.paginator;
    });
  }

  ngOnInit(): void {
  }

  openDialog(element): void {
    const dialogRef = this.dialog.open(FacturacionDetalleComponent, {
      height: '80vh',
      data: element
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }


}

export interface Factura {
  id: string;
  name: string;
  amount_with_breakdown: any;
  payer_name: any;
  payer_email: string;
  time: string;
}



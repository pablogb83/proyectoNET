import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatTableDataSource } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { PuertaService } from 'src/app/core/services/puerta.service';
import Swal from 'sweetalert2';
import { PuertaAddComponent } from '../puerta-add/puerta-add.component';
import { PuertaEditComponent } from '../puerta-edit/puerta-edit.component';

@Component({
  selector: 'app-puerta-list',
  templateUrl: './puerta-list.component.html',
  styleUrls: ['./puerta-list.component.css']
})
export class PuertaListComponent implements OnInit {

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  displayedColumns: string[] = ['id','denominacion','acciones'];

  idedificio?: number;
  edificio?: any; 
  edificioNombre: string;
  PuertaList:any=[];

  constructor(private service: EdificiosService,public dialog: MatDialog,private route: ActivatedRoute,private puertaService: PuertaService) { 
    this.route.queryParams.subscribe(params => {
      this.idedificio=params.idedificio;
      this.service.getEdificio(this.idedificio).subscribe(data=>{
        console.log(data);
        this.edificio = new MatTableDataSource<Edificio>(data);
        this.edificioNombre = this.edificio._data._value.nombre;
      });
    });
  }

  ngOnInit() {
    this.getPuertas();
  }

  getPuertas(): void{
    this.service.getPuertasEdificio(this.idedificio).subscribe(data=>{
      console.log(data);
      this.PuertaList = new MatTableDataSource<Puerta>(data);
      this.PuertaList.paginator = this.paginator;
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(PuertaAddComponent, {
      width: '250px',
      data: {idedificio: this.idedificio}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      //this.getSalones();
    }); 
  }

  openDialogUpdate(puerta:any): void {
    const dialogRef = this.dialog.open(PuertaEditComponent, {
      width: '250px',
      data: {id: puerta.id, denominacion: puerta.denominacion}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getPuertas();
    });
  }

  deleteClick(item:any){
    Swal.fire({
       title: 'Estas seguro?',
       text: "Este cambio sera irreversible",
       icon: 'warning',
       showCancelButton: true,
       confirmButtonColor: '#3085d6',
       cancelButtonColor: '#d33',
       confirmButtonText: 'Si, borrar!'
     }).then((result) => {
       if (result.isConfirmed) {
         this.puertaService.deletePuerta(item.id).subscribe(data=>{
           this.getPuertas()
         })
         Swal.fire(
           'Borrado!',
           'La puerta ha sido eliminada.',
           'success'
         )
       }
     })
   }

}

export interface Puerta {
  id: string;
  denominacion: string;
}

export interface Edificio{
  id:string;
  direccion: string;
  nombre: string;
  telefono: string;
}

export interface EdificioIdData {
  idedificio: number;
}

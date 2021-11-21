import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatTableDataSource } from '@angular/material';
import { EventosService } from 'src/app/core/services/eventos.service';
import Swal from 'sweetalert2';
import { EventosAddComponent } from '../eventos-add/eventos-add.component';
import { EventosEditComponent } from '../eventos-edit/eventos-edit.component';

@Component({
  selector: 'app-eventos-list',
  templateUrl: './eventos-list.component.html',
  styleUrls: ['./eventos-list.component.css']
})
export class EventosListComponent implements OnInit {

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  displayedColumns: string[] = ['id','nombre', 'descripcion', 'fechainicio', 'fechafin', 'acciones'];

  constructor(private service: EventosService,public dialog: MatDialog) {
    this.getEventos();
  }

  EventoList:any=[];

  ngOnInit() {
  }

  getEventos(): void{
    this.service.getEventos().subscribe(data=>{

      this.EventoList = new MatTableDataSource<Evento>(data);
      for (let fecha of data){
        fecha.fechaInicioEvt = fecha.fechaInicioEvt.substr(0,10);
        fecha.fechaFinEvt = fecha.fechaFinEvt.substr(0,10);
      }

      this.EventoList.paginator = this.paginator;
    });
  }
  
  openDialog(): void {
    const dialogRef = this.dialog.open(EventosAddComponent, {
      width: '500px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getEventos();
    }); 
  }

  openDialogUpdate(evt:any): void {
    const dialogRef = this.dialog.open(EventosEditComponent, {
      width: '500px',
      data: {id: evt.id, nombre: evt.nombre, descripcion: evt.descripcion, fechaInicioEvt: evt.fechaInicioEvt, fechaFinEvt: evt.fechaFinEvt, photoFileName: evt.photoFileName}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getEventos();
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
        this.service.deleteEvento(item.id).subscribe(data=>{
          this.getEventos()
        })
        Swal.fire(
          'Borrado!',
          'El evento ha sido eliminado.',
          'success'
        )
      }
    })
  }
}

export interface Evento {
  id: string;
  nombre: string;
  descripcion: string;
  fechaInicioEvt: Date;
  fechaFinEvt: Date;
  photoFileName: string;
}

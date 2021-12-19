import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CalendarioComponent } from 'src/app/calendario/calendario.component';
import { EventosService } from 'src/app/core/services/eventos.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { UsuarioEdificioService } from 'src/app/core/services/usuario-edificio.service';
import { Edificio } from 'src/app/edificios/edificios-list/edificios-list.component';
import { Salon } from 'src/app/salon/salon-list/salon-list.component';
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
  @ViewChild(MatSort,{static: false}) sort: MatSort;

  permiso: boolean = true;

  displayedColumns: string[] = ['id','nombre', 'descripcion', 'fechainicio', 'fechafin','horainicio','horafin','edificio','salon', 'acciones'];

  constructor(private service: EventosService,public dialog: MatDialog,private usuarioEdificioService: UsuarioEdificioService, private tokenService: TokenStorageService) {
    this.getEventos();
  }

  EventoList:any=[];

  ngOnInit() {
  }

  getEventos(){
    if(this.tokenService.getRoleName()==="ADMIN"){
      this.getEventosAdmin();
    }
    else if(this.tokenService.getRoleName()==="GESTOR"){
      this.usuarioEdificioService.getEdificioUsuario().subscribe(data=>{
        this.permiso = !!data;
        if(this.permiso){
          this.service.getEventosEdificio().subscribe(data=>{
            this.EventoList = new MatTableDataSource<Evento>(data);
            this.EventoList.paginator = this.paginator;
            this.EventoList.sort = this.sort;
          })
        }
      });
    }
  }

  getEventosAdmin(): void{
    this.service.getEventos().subscribe(data=>{
      this.EventoList = new MatTableDataSource<Evento>(data);
      this.EventoList.paginator = this.paginator;
      this.EventoList.sort = this.sort;
    });
  }
  
  openDialog(): void {
    const dialogRef = this.dialog.open(EventosAddComponent, {
      width: '50%',
      maxHeight:'80vh'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getEventos();
    }); 
  }

  openDialogUpdate(evt:Evento): void {
    const dialogRef = this.dialog.open(EventosEditComponent, {
      width: '500px',
      data: evt
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getEventos();
    });
  }

  openDialogCalendar(): void {
    const dialogRef = this.dialog.open(CalendarioComponent, {
      width: '80vw',
      height:'95vh',
      data: this.EventoList.data
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
          Swal.fire(
            'Borrado!',
            'El evento ha sido eliminado.',
            'success'
          )
          this.getEventos()
        })
      }
    })
  }
}

export interface Evento {
  id: string;
  nombre: string;
  descripcion: string;
  fechaInicioEvt: string;
  fechaFinEvt: string;
  salon: Salon,
  edificio: Edificio
}

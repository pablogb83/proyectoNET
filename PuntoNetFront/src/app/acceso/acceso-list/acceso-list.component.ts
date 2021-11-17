import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatTableDataSource } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { AccesoService } from 'src/app/core/services/acceso.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { UsuarioPuertaService } from 'src/app/core/services/usuario-puerta.service';
import { Persona } from 'src/app/persona/persona-list/persona-list.component';
import { Puerta } from 'src/app/puerta/puerta-list/puerta-list.component';
import { ReconocimientoFacialComponent } from 'src/app/reconocimiento-facial/reconocimiento-facial.component';
import Swal from 'sweetalert2';
import { AccesoAddComponent } from '../acceso-add/acceso-add.component';

@Component({
  selector: 'app-acceso-list',
  templateUrl: './acceso-list.component.html',
  styleUrls: ['./acceso-list.component.css']
})
export class AccesoListComponent implements OnInit {

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  displayedColumns: string[] = ['id','fechaHora', 'persona', 'documento',  'acciones'];

  AccesosList:any=[];
  edificio?:any;
  puerta?:any;
  puertaid:string;
  rol:string;
  userId:string;

  constructor(public dialog: MatDialog,private route: ActivatedRoute,private accesoService: AccesoService, private tokenService: TokenStorageService, private usuarioPuertaService: UsuarioPuertaService) { }

  getAccesos():void{
    this.rol = this.tokenService.getRoleName();
    this.userId = this.tokenService.getUserId();
    if(this.rol === 'PORTERO'){
      this.usuarioPuertaService.getPuertaUser(this.userId).subscribe(data=>{
        this.puertaid = data.id;
        this.puerta = data.denominacion;
        this.edificio = data.edificio.nombre;
        this.accesoService.getAccesosPuerta(this.puertaid).subscribe(data=>{
          this.AccesosList = new MatTableDataSource<Acceso>(data);
          this.AccesosList.paginator = this.paginator;
          console.log(this.AccesosList);
        })
      })
    }
  }

  ngOnInit() {
    this.getAccesos();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AccesoAddComponent, {
      width: '250px',
      data: {idpuerta: this.puertaid}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      //this.getSalones();
    }); 
  }

  openDialogFacial(): void {
    const dialogRef = this.dialog.open(ReconocimientoFacialComponent, {
      width: '1000px',
      data: {idpuerta: this.puertaid}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getAccesos();
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
         this.accesoService.deleteAcceso(item.id).subscribe(data=>{
           this.getAccesos()
         })
         Swal.fire(
           'Borrado!',
           'El registro ha sido eliminado.',
           'success'
         )
       }
     })
   }
}

export interface Data {
  idpuerta: string;
}
export interface Acceso {
  id: string;
  fechaHora: string;
  persona: Persona;
  puerta: Puerta;
}




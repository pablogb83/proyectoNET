import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { PuertaService } from 'src/app/core/services/puerta.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { UsuarioPuertaService } from 'src/app/core/services/usuario-puerta.service';
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
  rol:string;
  userId:string;
  puerta?: any;
  puertaid:string;

  constructor(private service: EdificiosService,public dialog: MatDialog,private route: ActivatedRoute,private puertaService: PuertaService, private tokenService: TokenStorageService, private usuarioPuertaService: UsuarioPuertaService, private router:Router, private handleError: HandleErrorsService) { 
    this.route.queryParams.subscribe(params => {
      this.idedificio=params.idedificio;
      this.service.getEdificio(this.idedificio).subscribe(data=>{
        this.edificio = data;
        if(!this.idedificio){
          this.idedificio = this.edificio.id;
        }
        this.edificioNombre = this.edificio.nombre;
        this.getPuertas();
      },err=>{
        this.router.navigate(["error"]);
      });
    });
    this.rol = this.tokenService.getRoleName();
    this.userId = this.tokenService.getUserId();
    if(this.rol === 'PORTERO'){
      this.usuarioPuertaService.getPuertaUser(this.userId).subscribe(data=>{
        this.puerta = new MatTableDataSource<Puerta>(data)
        if(this.puerta._data._value){
          this.puertaid = this.puerta._data._value.id;
        }
      })
    }
  }

  ngOnInit() {
  }

  refreshPage() {
    window.location.reload();
   }

  getPuertas(): void{
    this.service.getPuertasEdificio(this.idedificio).subscribe(data=>{
      console.log("Puertas: ", data);
      this.PuertaList = new MatTableDataSource<Puerta>(data);
      this.PuertaList.paginator = this.paginator;
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(PuertaAddComponent, {
      width: 'auto',
      data: {idedificio: this.idedificio}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getPuertas();
    }); 
  }

  openDialogUpdate(puerta:any): void {
    const dialogRef = this.dialog.open(PuertaEditComponent, {
      width: 'auto',
      data: {id: puerta.id, denominacion: puerta.denominacion}
    });

    dialogRef.afterClosed().subscribe(result => {
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
           this.handleError.showSuccessAlert("Se elimino la puerta correctamente");
           this.getPuertas()
         },err=>{
           this.handleError.showErrors(err);
         })
       }
     })
   }

   seleccionarPuerta(puerta:any):void{
      this.usuarioPuertaService.addUserPuerta(this.userId, puerta.id).subscribe(data=>{
        console.log(data);
        this.showSuccessAlert();
        this.refreshPage();
      },err=>{
        this.showErrorAlert(err.error);
        //this.refreshPage();
      });
   }

   liberarPuerta():void{
     this.usuarioPuertaService.deletePuertaUser(this.userId).subscribe(data=>{
      this.showSuccessAlertLiberar();
      this.refreshPage();
     },err=>{
       this.showErrorAlert(err.error);
       //this.refreshPage();
     })
   }

   showSuccessAlertLiberar() {
    Swal.fire('OK', 'Puerta liberada correctamente' , 'success');
  }

   showSuccessAlert() {
    Swal.fire('OK', 'Puerta asignada correctamente' , 'success');
  }

  showErrorAlert(msg:any) {
    Swal.fire('Error!', msg, 'error');
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

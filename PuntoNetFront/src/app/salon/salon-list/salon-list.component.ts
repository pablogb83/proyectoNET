import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { SalonService } from 'src/app/core/services/salon.service';
import Swal from 'sweetalert2';
import { SalonAddComponent } from '../salon-add/salon-add.component';
import { SalonEditComponent } from '../salon-edit/salon-edit.component';

@Component({
  selector: 'app-salon-list',
  templateUrl: './salon-list.component.html',
  styleUrls: ['./salon-list.component.css']
})
export class SalonListComponent implements OnInit {

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  displayedColumns: string[] = ['id','denominacion', 'numero','acciones'];
  
  idedificio?: number;
  edificio?: any; 
  edificioNombre: string;
  SalonList:any=[];

  constructor(private service: EdificiosService,public dialog: MatDialog,private route: ActivatedRoute, private salonService: SalonService, private router: Router, private handleError: HandleErrorsService) {
    this.route.queryParams.subscribe(params => {
      this.idedificio=params.idedificio;
      this.service.getEdificio(this.idedificio).subscribe(data=>{
        console.log(data);
        this.edificio = new MatTableDataSource<Edificio>(data);
        this.edificioNombre = this.edificio._data._value.nombre;
      },err=>{
        router.navigate(["error"]);
      });
    });
   }

  ngOnInit() {
    this.getSalones();
  }

  getSalones(): void{
    this.service.getSalonesEdificio(this.idedificio).subscribe(data=>{
      console.log(data);
      this.SalonList = new MatTableDataSource<Salon>(data);
      this.SalonList.paginator = this.paginator;
    });
  }
  
  openDialog(): void {
    const dialogRef = this.dialog.open(SalonAddComponent, {
      width: 'auto',
      data: {idedificio: this.idedificio}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getSalones();
    }); 
  }

  openDialogUpdate(salon:any): void {
    const dialogRef = this.dialog.open(SalonEditComponent, {
      width: 'auto',
      data: {id: salon.id, denominacion: salon.denominacion, numero: salon.numero}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getSalones();
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
        this.salonService.deleteSalon(item.id).subscribe((data:any)=>{
          this.handleError.showSuccessAlert(data.message)
          this.getSalones()
        },err=>{
          this.handleError.showErrors(err);
        })
       
      }
    })
  }

}

export interface Salon {
  id: string;
  denominacion: string;
  numero: string;
  edificio: Edificio;
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

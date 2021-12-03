import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import Swal from 'sweetalert2';
import { EdificiosAddComponent } from '../edificios-add/edificios-add.component';
import { EdificiosEditComponent } from '../edificios-edit/edificios-edit.component';

@Component({
  selector: 'app-edificios-list',
  templateUrl: './edificios-list.component.html',
  styleUrls: ['./edificios-list.component.css']
})
export class EdificiosListComponent implements OnInit {

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  displayedColumns: string[] = ['id','nombre', 'direccion', 'telefono', 'acciones'];

  constructor(private service: EdificiosService,public dialog: MatDialog, private router: Router) {
    this.getEdificios();
  }

  EdificioList:any=[];

  ngOnInit() {
  }

  getEdificios(): void{
    this.service.getEdificios().subscribe(data=>{
      console.log(data);
      this.EdificioList = new MatTableDataSource<Edificio>(data);
      this.EdificioList.paginator = this.paginator;
    });
  }
  
  openDialog(): void {
    const dialogRef = this.dialog.open(EdificiosAddComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getEdificios();
    }); 
  }

  openDialogUpdate(edif:any): void {
    const dialogRef = this.dialog.open(EdificiosEditComponent, {
      width: '250px',
      data: {id: edif.id, nombre: edif.nombre, direccion: edif.direccion, telefono: edif.telefono}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getEdificios();
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
        this.service.deleteEdificio(item.id).subscribe(data=>{
          this.getEdificios()
        })
        Swal.fire(
          'Borrado!',
          'El edificio ha sido eliminado.',
          'success'
        )
      }
    })
  }

  redirectSalones(idedificio:any): void{
    this.router.navigate(['salones'], { queryParams: { idedificio } });
  }

  redirectPuertas(idedificio:any): void{
    this.router.navigate(['puertas'], { queryParams: { idedificio } });
  }
  
}


export interface Edificio {
  id: string;
  nombre: string;
  direccion: string;
  telefono: string;
}


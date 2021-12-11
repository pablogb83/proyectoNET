import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import { InstitucionService } from 'src/app/core/services/institucion.service';
import Swal from 'sweetalert2';
import { InstAddComponent } from '../inst-add/inst-add.component';
import { InstEditComponent } from '../inst-edit/inst-edit.component';
import {MatTableDataSource} from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { AdminAddComponent } from '../admin-add/admin-add.component';


export interface DialogData {
  id: string;
  nombre: string;
  direccion: string;
  telefono:string;
}

@Component({
  selector: 'app-institucion-list',
  templateUrl: './institucion-list.component.html',
  styleUrls: ['./institucion-list.component.css']
})
export class InstitucionListComponent implements AfterViewInit {

  constructor(private service:InstitucionService,public dialog: MatDialog) { 
    this.getInstituciones();
  }

  InstitucionList:any=[];

  displayedColumns: string[] = ['nombre', 'direccion', 'telefono', 'acciones'];

  inst:any;

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  ngAfterViewInit() {
    this.InstitucionList.paginator = this.paginator;
     console.log(this.InstitucionList);
  }

  // ngOnInit(): void {
  //   this.refreshInstList();
  // }


  getInstituciones(){
    this.service.getInstList().subscribe(data=>{
      this.InstitucionList = new MatTableDataSource<Instituciones>(data);
      this.InstitucionList.paginator = this.paginator;
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(InstAddComponent, {
      width: '500px',
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getInstituciones();
    });
  }

  openDialogUpdate(inst:any): void {
    const dialogRef = this.dialog.open(InstEditComponent, {
      width: '250px',
      data: {id: inst.id, nombre: inst.name, direccion: inst.direccion, telefono: inst.telefono}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getInstituciones();
    });
  }
  
  openDialogAdmin(inst: any): void {
    const dialogRef = this.dialog.open(AdminAddComponent, {
      width: '250px',
      data: {id: inst.id}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
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
        this.service.deleteInst(item.id).subscribe(data=>{
          this.getInstituciones();
        })
        Swal.fire(
          'Borrado!',
          'La institucion ha sido eliminada.',
          'success'
        )
      }
    })
  }
}

export interface Instituciones {
  connectionString: string;
  direccion: string;
  id: string;
  identifier: string;
  name: string;
  telefono: string;
}

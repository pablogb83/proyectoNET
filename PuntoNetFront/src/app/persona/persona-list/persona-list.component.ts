import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { PersonaService } from 'src/app/core/services/persona.service';
import Swal from 'sweetalert2';
import { PersonaAddComponent } from '../persona-add/persona-add.component';
import { PersonaAltaMasivaComponent } from '../persona-alta-masiva/persona-alta-masiva.component';
import { PersonaEditComponent } from '../persona-edit/persona-edit.component';

@Component({
  selector: 'app-persona-list',
  templateUrl: './persona-list.component.html',
  styleUrls: ['./persona-list.component.css']
})
export class PersonaListComponent implements OnInit {

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;
  @ViewChild(MatSort,{static: false}) sort: MatSort;

  displayedColumns: string[] = ['id','nombres', 'apellidos', 'telefono', 'email', 'tipo_doc', 'nro_doc' ,'acciones'];

  PersonasList:any=[];

  constructor(private service: PersonaService,public dialog: MatDialog, private router: Router) {
    
   }

  ngOnInit() {
    this.getPersonas()
  }

  getPersonas(): void{
    this.service.getPersonas().subscribe(data=>{
      this.PersonasList = new MatTableDataSource<Persona>(data);
      this.PersonasList.paginator = this.paginator;
      this.PersonasList.sort = this.sort;
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(PersonaAddComponent, {
      width: "50%",
      maxHeight:'80vh',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getPersonas();
    }); 
  }

  openDialogAltaMasiva(): void {
    const dialogRef = this.dialog.open(PersonaAltaMasivaComponent, {
      width: '30%',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getPersonas();
    }); 
  }

  openDialogUpdate(prs:any): void {
    const dialogRef = this.dialog.open(PersonaEditComponent, {
      width: "50%",
      maxHeight:'80vh',
      data: {id: prs.id, nombres: prs.nombres, apellidos: prs.apellidos, telefono: prs.telefono, email: prs.email, tipo_doc: prs.tipo_doc, nro_doc: prs.nro_doc, photoFileName: prs.photoFileName}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getPersonas();
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
        this.service.deletePersona(item.id).subscribe(data=>{
          this.getPersonas()
        })
        Swal.fire(
          'Borrado!',
          'El registro ha sido eliminado.',
          'success'
        )
      }
    })
  }

  busqueda(event: any): void{
    console.log('Estoy buuscando', event.target.value);
    this.getPersonasBusqueda(event.target.value);
  }

  getPersonasBusqueda(filter: string): void{
    if(filter===''){
      this.getPersonas();
    }else{
      this.service.getPersonasBusqueda(filter).subscribe(data=>{
        this.PersonasList = new MatTableDataSource<Persona>(data);
        this.PersonasList.paginator = this.paginator;
        this.PersonasList.sort = this.sort;
      });
    }
    
  }

}

export interface Persona {
  id: string;
  nombres: string;
  apellidos: string;
  telefono: string;
  email: string;
  tipo_doc: string;
  nro_doc: string
  photoFileName: string;
}

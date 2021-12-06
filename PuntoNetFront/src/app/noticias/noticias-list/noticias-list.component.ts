import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatTableDataSource } from '@angular/material';
import { NoticiasService } from 'src/app/core/services/noticias.service';
import Swal from 'sweetalert2';
import { NoticiasAddComponent } from '../noticias-add/noticias-add.component';
import { NoticiasEditComponent } from '../noticias-edit/noticias-edit.component';

@Component({
  selector: 'app-noticias-list',
  templateUrl: './noticias-list.component.html',
  styleUrls: ['./noticias-list.component.css']
})
export class NoticiasListComponent implements OnInit {

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  displayedColumns: string[] = ['id','nombre', 'publicadopor', 'fechapub', 'acciones'];

  constructor(private service: NoticiasService, public dialog: MatDialog) {
    this.getNoticias();
  }

  NoticiasList:any=[];

  ngOnInit() {
  }

  getNoticias(): void{
    this.service.getNoticias().subscribe(data=>{

      this.NoticiasList = new MatTableDataSource<Noticias>(data);
      console.log(data);
      for (let d of data){
        d.fechaPublicacion = d.fechaPublicacion.substr(0,10);
      }

      this.NoticiasList.paginator = this.paginator;
    });
  }
  
  openDialog(): void {
    const dialogRef = this.dialog.open(NoticiasAddComponent, {
      width: '900px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getNoticias();
    }); 
  }


  // openDialogUpdate(not:any): void {
  //   const dialogRef = this.dialog.open(NoticiasEditComponent, {
  //     width: '500px',
  //     data: {id: not.id, nombre: not.nombre, descripcion: not.descripcion, PublicadoPor: not.PublicadoPor, FechaPublicacion: not.FechaPublicacion, photoFileName: not.photoFileName}
  //   });

  //   dialogRef.afterClosed().subscribe(result => {
  //     console.log('The dialog was closed');
  //     this.getNoticias();
  //   });
  // }

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
        this.service.deleteNoticia(item.id).subscribe(data=>{
          this.getNoticias()
        })
        Swal.fire(
          'Borrado!',
          'La noticia ha sido eliminada.',
          'success'
        )
      }
    })
  }

}

export interface Noticias {
  id: string;
  nombre: string;
  descripcion: string;
  PublicadoPor: string;
  fechaPublicacion: Date;
  photoFileName: string;
}

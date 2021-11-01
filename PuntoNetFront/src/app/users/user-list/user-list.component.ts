import { Component, OnInit, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { NGXLogger } from 'ngx-logger';
import { UsuariosService } from 'src/app/core/services/usuarios.service';
import { MatDialog, MatPaginator, MatTableDataSource } from '@angular/material';
import { UserRoleComponent } from '../user-role/user-role.component';
import Swal from 'sweetalert2';
import { UsersAddComponent } from '../users-add/users-add.component';
import { UsersEditComponent } from '../users-edit/users-edit.component';
import { UserEdificioComponent } from '../user-edificio/user-edificio.component';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { UsuarioEdificioService } from 'src/app/core/services/usuario-edificio.service';


export interface DialogData {
  user: any
}

export interface DialogDataUser {
  id: number;
  email:string;
  passwordPlano:string;
}

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  UsuariosList:any=[];
  edificios: any=[];
  selectedValue: string;
  todos:string;

  displayedColumns: string[] = ['id','email', 'rol', 'acciones'];

  constructor(
    private logger: NGXLogger,
    private titleService: Title,
    private service: UsuariosService,
    public dialog: MatDialog,
    private edificioService:EdificiosService,
    private usuariosEdificioService:UsuarioEdificioService
  ) {
    this.edificioService.getEdificios().subscribe(data=>{
      this.edificios = data
    });
   }

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  ngOnInit() {
    this.titleService.setTitle('angular-material-template - Users');
    this.getUsuarios();
  }

  getUsuarios(): void{
    this.service.getUsuarios().subscribe(data=>{
      console.log(data);
      this.UsuariosList = new MatTableDataSource<Usuarios>(data);
      this.UsuariosList.paginator = this.paginator;
    });
  }

  listarPorEdificio(idEdificio:any){
    console.log(idEdificio);
    if(idEdificio===0){
      this.getUsuarios();
    }
    else{
      this.usuariosEdificioService.getUsuariosEdificio(idEdificio).subscribe(data=>{
        console.log(data);
        this.UsuariosList = new MatTableDataSource<Usuarios>(data);
        this.UsuariosList.paginator = this.paginator;
      });
    }
  }

  openDialogAsignarRol(user:any): void {
    const dialogRef = this.dialog.open(UserRoleComponent, {
      width: '250px',
      data: {user: user}
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getUsuarios();
    }); 
  }

  openDialogAsignarEdificio(user:any): void {
    const dialogRef = this.dialog.open(UserEdificioComponent, {
      width: '250px',
      data: {user: user}
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getUsuarios();
    }); 
  }
  
  openDialog(): void {
    const dialogRef = this.dialog.open(UsersAddComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getUsuarios();
    }); 
  }

  openDialogUpdate(user:any): void {
    const dialogRef = this.dialog.open(UsersEditComponent, {
      width: '250px',
      data: {id: user.id, email: user.email}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getUsuarios();
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
        this.service.deleteUsuario(item.id).subscribe(data=>{
          this.getUsuarios()
        })
        Swal.fire(
          'Borrado!',
          'El usuario ha sido eliminado.',
          'success'
        )
      }
    })
  }

}



export interface Usuarios {
  id: number,
  email: string,
  role: string
}
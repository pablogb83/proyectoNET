import { Component, OnInit, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { UsuariosService } from 'src/app/core/services/usuarios.service';
import { UserRoleComponent } from '../user-role/user-role.component';
import Swal from 'sweetalert2';
import { UsersAddComponent } from '../users-add/users-add.component';
import { UsersEditComponent } from '../users-edit/users-edit.component';
import { UserEdificioComponent } from '../user-edificio/user-edificio.component';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { UsuarioEdificioService } from 'src/app/core/services/usuario-edificio.service';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { ActivatedRoute } from '@angular/router';
import { AdminAddComponent } from 'src/app/institucion/admin-add/admin-add.component';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';


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
  institucion: string;
  role: string;

  displayedColumns: string[] = ['id','email', 'rol', 'acciones'];

  constructor(
    private titleService: Title,
    private service: UsuariosService,
    public dialog: MatDialog,
    private edificioService:EdificiosService,
    private usuariosEdificioService:UsuarioEdificioService,
    private tokenStorageService: TokenStorageService,
    public route: ActivatedRoute,
    private handleError: HandleErrorsService
  ) {
    this.route.queryParams.subscribe(data=>{
      console.log("La institucion es: ", data);
      this.institucion = data.institucion;
    });
 

   }

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  ngOnInit() {
    this.getUsuarios();
  }

  getUsuarios(): void{

    this.role = this.tokenStorageService.getRoleName();
    if(this.role==="ADMIN"){
      this.edificioService.getEdificios().subscribe(data=>{
        this.edificios = data
      });
      this.titleService.setTitle('Usuarios');
      this.service.getUsuariosAdmin().subscribe(data=>{
        this.UsuariosList = new MatTableDataSource<Usuarios>(data);
        this.UsuariosList.paginator = this.paginator;
      });
    }
    else if(this.role==="SUPERADMIN"){
      this.service.getUsuariosInstitucion(this.institucion).subscribe(data=>{
        this.UsuariosList = new MatTableDataSource<Usuarios>(data);
        this.UsuariosList.paginator = this.paginator;
      });
    }
    
  }

  listarPorEdificio(idEdificio:any){
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
      width: 'auto',
      data: {user: user}
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getUsuarios();
    }); 
  }

  openDialogAsignarEdificio(user:any): void {
    const dialogRef = this.dialog.open(UserEdificioComponent, {
      width: 'auto',
      data: {user: user}
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getUsuarios();
    }); 
  }
  
  openDialog(): void {
    if(this.role==="ADMIN"){
      const dialogRef = this.dialog.open(UsersAddComponent, {
        width: 'auto',
      });
  
      dialogRef.afterClosed().subscribe(result => {
        this.getUsuarios();
      }); 
    }
    else if(this.role==="SUPERADMIN"){
      const dialogRef = this.dialog.open(AdminAddComponent, {
        width: 'auto',
        data:{institucion: this.institucion}
      });
  
      dialogRef.afterClosed().subscribe(result => {
        this.getUsuarios();
      }); 
    }
  
  }

  openDialogUpdate(user:any): void {
    const dialogRef = this.dialog.open(UsersEditComponent, {
      width: 'auto',
      data: {id: user.id, email: user.email}
    });

    dialogRef.afterClosed().subscribe(result => {
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
        if(this.role==="ADMIN"){
          this.service.deleteUsuario(item.id).subscribe((data:any)=>{
            this.handleError.showSuccessAlert(data.message)
            this.getUsuarios()
          })
        }
        else if(this.role==="SUPERADMIN"){
          this.service.deleteAdmin(item.id).subscribe((data:any)=>{
            this.handleError.showSuccessAlert(data.message)
            this.getUsuarios()
          })
        }
     
      }
    })
  }

}

export interface Usuarios {
  id: number,
  email: string,
  role: string
}
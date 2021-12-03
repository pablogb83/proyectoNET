import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RolesService } from 'src/app/core/services/roles.service';
import { RolesAddComponent } from 'src/app/roles/roles-add/roles-add.component';
import { RolesEditComponent } from 'src/app/roles/roles-edit/roles-edit.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  displayedColumns: string[] = ['id','nombre','acciones'];

  constructor(private service: RolesService,public dialog: MatDialog) {
    this.getRoles();
  }

  RoleList:any=[];

  ngOnInit() {
  }

  getRoles(): void{
    this.service.getRoles().subscribe(data=>{
      console.log(data);
      this.RoleList = new MatTableDataSource<Role>(data);
      this.RoleList.paginator = this.paginator;
    });
  }
  
  openDialog(): void {
    const dialogRef = this.dialog.open(RolesAddComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getRoles();
    }); 
  }

  openDialogUpdate(role:any): void {
    const dialogRef = this.dialog.open(RolesEditComponent, {
      width: '250px',
      data: {id: role.id, nombre: role.nombreRol}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.getRoles();
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
        this.service.deleteRole(item.id).subscribe(data=>{
          this.getRoles()
        })
        Swal.fire(
          'Borrado!',
          'El Role ha sido eliminado.',
          'success'
        )
      }
    })
  }

}

export interface Role {
  id: string;
  nombre: string;
}

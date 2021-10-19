import { Component, OnInit, ViewChild } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { NotificationService } from '../../core/services/notification.service';
import { NGXLogger } from 'ngx-logger';
import { UsuariosService } from 'src/app/core/services/usuarios.service';
import { MatDialog, MatPaginator, MatTableDataSource } from '@angular/material';
import { UserRoleComponent } from '../user-role/user-role.component';


export interface DialogData {
  user: any
}

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  UsuarioList:any=[];

  displayedColumns: string[] = ['id','email', 'rol', 'acciones'];

  constructor(
    private logger: NGXLogger,
    private notificationService: NotificationService,
    private titleService: Title,
    private usuariosService: UsuariosService,
    public dialog: MatDialog
  ) { }

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  ngOnInit() {
    this.titleService.setTitle('angular-material-template - Users');
    this.logger.log('Users loaded');
    this.usuariosService.getUsuarios().subscribe(data=>{
      console.log(data);
      this.UsuarioList = new MatTableDataSource<Usuarios>(data);
      this.UsuarioList.paginator = this.paginator;
    });
  }

  openDialogAsignarRol(user:any): void {
    const dialogRef = this.dialog.open(UserRoleComponent, {
      width: '250px',
      data: {user: user}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
}



export interface Usuarios {
  id: string,
  email: string;
}
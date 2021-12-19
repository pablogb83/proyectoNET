import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { RolesService } from 'src/app/core/services/roles.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {

  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  displayedColumns: string[] = ['id','nombre'];

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
}

export interface Role {
  id: string;
  nombre: string;
}

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { HandleErrorsService } from 'src/app/core/services/handle.errors.service';
import { RolesService } from 'src/app/core/services/roles.service';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-roles-add',
  templateUrl: './roles-add.component.html',
  styleUrls: ['./roles-add.component.css']
})
export class RolesAddComponent implements OnInit {

  nombre?:string;

  onNoClick(): void {
    this.dialogRef.close();
  }


  constructor(public dialogRef: MatDialogRef<RolesAddComponent>, @Inject(MAT_DIALOG_DATA) public data: DialogData, private service:RolesService, private handleError: HandleErrorsService) { }

  ngOnInit() {
  }

  agregarRole(){
    var val = {
      nombre:this.nombre,  
    };
    this.service.postRole(val.nombre).subscribe((res:any)=>{
      this.handleError.showSuccessAlert(res.message)
    }, err =>{
      this.handleError.showErrors(err);
    });
  }

}

import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DashboardHomeComponent } from '../dashboard-home/dashboard-home.component';

@Component({
  selector: 'app-dashborad-get-noticia',
  templateUrl: './dashborad-get-noticia.component.html',
  styleUrls: ['./dashborad-get-noticia.component.css']
})
export class DashboradGetNoticiaComponent implements OnInit {

  @Input() editable: boolean = false;

  nombre: string;
  descripcion: string;
  fechaPublicacion: Date;
  PhotoFilePath: string;
  publicadoPor: string;

  constructor(public dialogRef: MatDialogRef<DashboradGetNoticiaComponent>, 
                    @Inject(MAT_DIALOG_DATA) public data: DashboardHomeComponent) {
    
    this.nombre = data.nombre;
    this.descripcion = data.descripcion;
    let s = new Date(data.fechaPublicacion)
    this.fechaPublicacion = new Date(data.fechaPublicacion);
    this.publicadoPor = data.PublicadoPor;
    this.PhotoFilePath = data.PhotoFilePath;
    

   }

   onNoClick(): void {
    this.dialogRef.close();
  }


  ngOnInit() {

  }

}

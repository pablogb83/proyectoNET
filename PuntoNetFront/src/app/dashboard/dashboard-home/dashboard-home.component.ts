import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatPaginator, MatTableDataSource } from '@angular/material';
import { FileService } from 'src/app/core/services/file.service';
import { NoticiasService } from 'src/app/core/services/noticias.service';


@Component({
  selector: 'app-dashboard-home',
  templateUrl: './dashboard-home.component.html',
  styleUrls: ['./dashboard-home.component.css']
})
export class DashboardHomeComponent implements OnInit {
  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  Cards? = [];
  PhotoFileName?: any;
  PhotoFilePath?:any;

  constructor(private service: NoticiasService, public dialog: MatDialog, private fileService:FileService) {
    this.getNoticias();
    // this.PhotoFileName = this.PhotoFileName.photoFileName;
    // this.PhotoFilePath=this.fileService.PhotoUrl+this.PhotoFileName;
  }

  getNoticias(): void{
  this.service.getNoticias().subscribe(data=>{
    console.log(data);
    this.Cards = data;
    this.PhotoFileName = data;
  });

}

// async getNoticias(): Promise<any> {
//   this.Cards = await this.service.getNoticias().subscribe();
//   console.log(this.Cards)
// }


  ngOnInit() {
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

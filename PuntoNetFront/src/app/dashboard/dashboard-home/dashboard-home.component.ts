import { Component, OnInit, ViewChild } from '@angular/core';
import { FileService } from 'src/app/core/services/file.service';
import { NoticiasService } from 'src/app/core/services/noticias.service';
import { Subscription, Observable } from 'rxjs';
import { DashboradGetNoticiaComponent } from '../dashborad-get-noticia/dashborad-get-noticia.component';
import { NotificationService } from 'src/app/core/services/notification.service';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { Title } from '@angular/platform-browser';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { Noticias } from 'src/app/noticias/noticias-list/noticias-list.component';
import { MatTableDataSource } from '@angular/material/table';
import { CalendarioComponent } from 'src/app/calendario/calendario.component';


@Component({
  selector: 'app-dashboard-home',
  templateUrl: './dashboard-home.component.html',
  styleUrls: ['./dashboard-home.component.css']
})
export class DashboardHomeComponent implements OnInit {

  ToString() {
    throw new Error('Method not implemented.');
  }
  


  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;

  currentUser: any;
  Cards:any=[];
  PhotoFileName:any=[];
  PhotoFilePath:any=[];


  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
    this.titleService.setTitle('PuertanFront');
    
  }


  constructor(private notificationService: NotificationService,
    private authService: AuthenticationService,
    private titleService: Title,private service: NoticiasService, public dialog: MatDialog, private fileService:FileService) {
    this.getNoticias();
  }


  getNoticias(): void{
  this.service.getUltimasNoticias().subscribe(data=>{

    this.Cards = data
    // this.Cards  = new MatTableDataSource<DashboardHomeComponent>(data);
    for (let card of this.Cards){
      card.PhotoFilePath =  this.fileService.PhotoUrl + card.photoFileName;
    }
    this.Cards.paginator = this.paginator;
    console.log(this.Cards);
  });

}

openDialogNoticia(noticia:any): void {
  const dialogRef = this.dialog.open(DashboradGetNoticiaComponent, {
    width: '700px',
    data: {nombre: noticia.nombre, descripcion: noticia.descripcion, fechaPublicacion: noticia.fechaPublicacion,
      PhotoFilePath: noticia.PhotoFilePath, publicadoPor: noticia.publicadoPor }
  });

  dialogRef.afterClosed().subscribe(result => {
    console.log('The dialog was closed');
    this.getNoticias();
  });
}



}

export interface DashboardHomeComponent {
  id: string;
  nombre: string;
  descripcion: string;
  PublicadoPor: string;
  fechaPublicacion: Date;
  photoFileName: string;
}

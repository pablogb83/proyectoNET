import { Component, OnInit } from '@angular/core';
import { FileService } from 'src/app/core/services/file.service';
import { InstitucionService } from 'src/app/core/services/institucion.service';
import { NoticiasService } from 'src/app/core/services/noticias.service';

@Component({
  selector: 'app-noticias-publicas',
  templateUrl: './noticias-publicas.component.html',
  styleUrls: ['./noticias-publicas.component.css']
})
export class NoticiasPublicasComponent implements OnInit {

  noticias: any[];
  instituciones: any[];
  institucion: string;

  selectedInst: any;

  longText = `The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog
  from Japan. A small, agile dog that copes very well with mountainous terrain, the Shiba Inu was
  originally bred for hunting.`;

  constructor(private noticiasService: NoticiasService, private fileService:FileService, private institucionService: InstitucionService) { }

  ngOnInit(): void {
    this.institucionService.getInstList().subscribe(data=>{
      this.instituciones = data;
      this.getInstituciones();
    });
    
  }



  getInstituciones(){
   
    this.noticiasService.getNoticiasPublicas(this.institucion).subscribe(data=>{
      this.noticias=data;
      console.log(this.noticias);
    });
  }

}

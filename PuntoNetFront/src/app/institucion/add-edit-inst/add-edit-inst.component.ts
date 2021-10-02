import { Component, Input, OnInit } from '@angular/core';
import { InstitucionService } from 'src/app/Services/institucion.service';


@Component({
  selector: 'app-add-edit-inst',
  templateUrl: './add-edit-inst.component.html',
  styleUrls: ['./add-edit-inst.component.css']
})
export class AddEditInstComponent implements OnInit {
  
  constructor(private service:InstitucionService) { }

  @Input() inst:any;
  id?:string;
  nombre?:string;
  direccion?:string;
  telefono?:string;

  ngOnInit(): void {
    this.id=this.inst.id;
    this.nombre=this.inst.nombre;
    this.direccion=this.inst.direccion;
    this.telefono=this.inst.telefono;
  }

  agregarInstitucion(){
    var val = {id:this.id,
              nombre:this.nombre,
              direccion:this.direccion,
              telefono:this.telefono};
    this.service.addInst(val).subscribe(res=>{
      alert("Institucion agregada");
    });
  }

  updateInstitucion(){
    var id = this.id;
    //var idcast: number = +id;
    
    var val = {
              nombre:this.nombre,
              direccion:this.direccion,
              telefono:this.telefono};

    this.service.updateInst(Number(id), val).subscribe(res=>{
    console.log(res);
    });
  }

}

import { Component, OnInit } from '@angular/core';
import { InstitucionService } from 'src/app/Services/institucion.service';


@Component({
  selector: 'app-show-inst',
  templateUrl: './show-inst.component.html',
  styleUrls: ['./show-inst.component.css']
})
export class ShowInstComponent implements OnInit {

  constructor(private service:InstitucionService) { }

  InstitucionList:any=[];

  ModalTitle?:string;

  ActivateAddEditInstComp:boolean=false;
  inst:any;


  InstitucionIdFilter:string="";
  InstitucionNameFilter:string="";
  InstitucionListWithoutFilter:any=[];

  ngOnInit(): void {
    this.refreshInstList();
  }

  addClick(){
    console.log('aca andamos')
    this.inst={
      id:0,
      nombre:"",
      direccion:"",
      telefono:""
    }
    this.ModalTitle="Agregar Institución";
    this.ActivateAddEditInstComp=true;

  }

  editClick(item:any){
    this.inst=item;
    this.ModalTitle="Editar Institución";
    this.ActivateAddEditInstComp=true;
  }

  deleteClick(item:any){
    if(confirm('Estás seguro??')){
      this.service.deleteInst(item.id).subscribe(data=>{
     //   alert(data.toString());
        this.refreshInstList();
      })
    }
  }

  closeClick(){
    this.ActivateAddEditInstComp=false;
    this.refreshInstList();
  }


  refreshInstList(){
    this.service.getInstList().subscribe(data=>{
      console.log(data);
      this.InstitucionList=data;
      this.InstitucionListWithoutFilter=data;
    });
  }

  FilterFn(){
    var InstitucionIdFilter = this.InstitucionIdFilter;
    var InstitucionNameFilter = this.InstitucionNameFilter;

    this.InstitucionList = this.InstitucionListWithoutFilter.filter(function (el:any){
        return el.id.toString().toLowerCase().includes(
          InstitucionIdFilter.toString().trim().toLowerCase()
        )&&
        el.nombre.toString().toLowerCase().includes(
          InstitucionNameFilter.toString().trim().toLowerCase()
        )
    });
  }

  sortResult(prop:any,asc:any){
    this.InstitucionList = this.InstitucionListWithoutFilter.sort(function(a:any,b:any){
      if(asc){
          return (a[prop]>b[prop])?1 : ((a[prop]<b[prop]) ?-1 :0);
      }else{
        return (b[prop]>a[prop])?1 : ((b[prop]<a[prop]) ?-1 :0);
      }
    })
  }

}

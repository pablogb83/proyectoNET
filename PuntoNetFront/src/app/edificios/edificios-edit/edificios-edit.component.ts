import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { InstEditComponent } from 'src/app/institucion/inst-edit/inst-edit.component';
import { DialogData } from 'src/app/institucion/institucion-list/institucion-list.component';
import { Edificio } from 'src/app/edificios/edificios-list/edificios-list.component';
import { environment } from 'src/environments/environment';
import * as Mapboxgl from 'mapbox-gl';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edificios-edit',
  templateUrl: './edificios-edit.component.html',
  styleUrls: ['./edificios-edit.component.css']
})
export class EdificiosEditComponent implements OnInit {

  id?:string;
  nombre?:string;
  direccion?:string;
  telefono?:string;
  mapa: Mapboxgl.Map;
  lng?:number;
  lat?:number;

  ngOnInit() {
    (Mapboxgl as any).accessToken = environment.mapboxKey;
    this.mapa = new Mapboxgl.Map({
      container: 'mapa-mapbox-edit', // container ID
      style: 'mapbox://styles/mapbox/streets-v11', // style URL
      center: [this.lng, this.lat], // LNG, LAT
      zoom: 13 // 15 starting zoom
    });

    this.crearMarcador(this.lng, this.lat);

  }

  constructor(public dialogRef: MatDialogRef<InstEditComponent>, @Inject(MAT_DIALOG_DATA) public data: Edificio, private service:EdificiosService) { 
    console.log(data);
    this.id = data.id;
    this.nombre = data.nombre;
    this.direccion = data.direccion;
    this.telefono = data.telefono;
    this.lat = Number(data.lat);
    this.lng = Number(data.lng);

  }

  crearMarcador(lng: number, lat: number){
    const marker = new Mapboxgl.Marker({
      draggable: true
      })
      .setLngLat([lng, lat])
      .addTo(this.mapa);

      marker.on('dragend', () =>{
        this.lng = marker.getLngLat().lng;
        this.lat = marker.getLngLat().lat;
        
        console.log(marker.getLngLat())
      })
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  updateEdificio(){
    var id = this.id;
    //var idcast: number = +id;
    
    var val = {
              nombre:this.nombre,
              direccion:this.direccion,
              telefono:this.telefono};

    this.service.putEdificio(Number(id), val.nombre,val.direccion,val.telefono, this.lng.toString(), this.lat.toString()).subscribe(res=>{
    this.showSuccessAlert();
    }, err=>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Institucion actualizada con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }


}

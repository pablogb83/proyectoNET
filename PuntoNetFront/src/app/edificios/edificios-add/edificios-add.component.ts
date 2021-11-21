import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { EdificiosService } from 'src/app/core/services/edificios.service';
import { Edificio } from 'src/app/edificios/edificios-list/edificios-list.component';
import { environment } from 'src/environments/environment';
import * as Mapboxgl from 'mapbox-gl';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edificios-add',
  templateUrl: './edificios-add.component.html',
  styleUrls: ['./edificios-add.component.css']
})
export class EdificiosAddComponent implements OnInit {

  nombre?:string;
  direccion?:string;
  telefono?:string;
  mapa: Mapboxgl.Map;
  lng: number;
  lat: number;

  ngOnInit() {
    (Mapboxgl as any).accessToken = environment.mapboxKey;
    this.mapa = new Mapboxgl.Map({
      container: 'mapa-mapbox', // container ID
      style: 'mapbox://styles/mapbox/streets-v11', // style URL
      center: [-56.678196, -34.3379394], // LNG, LAT
      zoom: 6 // starting zoom
    });

    this.crearMarcador(-56.678196, -34.3379394);


  }

  onNoClick(): void {
    this.dialogRef.close();
  }


  constructor(public dialogRef: MatDialogRef<EdificiosAddComponent>, @Inject(MAT_DIALOG_DATA) public data: Edificio, 
              private service:EdificiosService) { }


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


  agregarEdificio(){
    var val = {
              nombre:this.nombre,
              direccion:this.direccion,
              telefono:this.telefono
              };
    
    this.service.postEdificios(val.nombre,val.direccion,val.telefono, this.lng.toString(), this.lat.toString()).subscribe(res=>{
      this.showSuccessAlert();
    }, err =>{
      this.showErrorAlert();
    });
  }

  showSuccessAlert() {
    Swal.fire('OK', 'Edificio agregado con exito!', 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'Algo salió mal!', 'error');
  }

}

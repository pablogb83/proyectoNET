import { Component, ElementRef, EventEmitter, Inject, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {  Router } from '@angular/router';
import { Subject } from 'rxjs';
import Swal from 'sweetalert2';
import { Data } from '../acceso/acceso-list/acceso-list.component';
import { AccesoService } from '../core/services/acceso.service';
import { FileService } from '../core/services/file.service';
import { PersonaService } from '../core/services/persona.service';

@Component({
  selector: 'app-reconocimiento-facial',
  templateUrl: './reconocimiento-facial.component.html',
  styleUrls: ['./reconocimiento-facial.component.css']
})
export class ReconocimientoFacialComponent implements OnInit {

  FileName: string ="";
  imagePath: string;
  imgURL: any;
  FilePath: string ="";
  PhotoFilePath: string;
  idPuerta: any;
  showWebcam = true;
  isCameraExist = true;
  private trigger: Subject<void> = new Subject<void>();
  private nextWebcam: Subject<boolean | string> = new Subject<boolean | string>();
  constructor(private fileService: FileService, public dialogRef: MatDialogRef<ReconocimientoFacialComponent>,@Inject(MAT_DIALOG_DATA) public data: Data, private service:AccesoService, private personaService:PersonaService, public router: Router) { 
    this.idPuerta = data.idpuerta;
  }


  uploadFile(event){
    const files = event.target.files;
    var file=event.target.files[0];
    if (files.length === 0)
      return;

    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }

    var reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
      this.imgURL = reader.result; 
    }
    var file=event.target.files[0];
    const formData:FormData=new FormData();
    formData.append('uploadedFile',file,file.name);

    this.fileService.UploadfileFace(formData).subscribe((data: any)=>{
      console.log(data);
      this.showSuccessAlert(data.nombres);
      this.PhotoFilePath = this.fileService.PhotoUrl + data.photoFileName;
      this.agregarAcceso(data.id);
    },err=>{
      this.showErrorAlert();
    });
  }

  

  agregarAcceso(idPersona){
    var val = {
      fechaHora:new Date(),
      puertaId:this.idPuerta,
      personaId:idPersona,
      };
      this.service.postAcceso(val).subscribe(res=>{
      }, err =>{
      });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  showSuccessAlert(persona: string) {
    Swal.fire('OK', 'La persona de la imagen ingresada se encuentra en el sistema y es el usuario: ' + persona, 'success');
  }

  showErrorAlert() {
    Swal.fire('Error!', 'La persona no se encontro en el sistema', 'error');
  }

  
  ngOnInit(): void {

  }

  WIDTH = 640;
  HEIGHT = 480;

  @ViewChild("video",{ read: true,static:true})
  public video: ElementRef;

  @ViewChild("canvas",{ read: true,static:true})
  public canvas: ElementRef;

  captures: string[] = [];
  error: any;
  isCaptured: boolean;

  async ngAfterViewInit() {
    await this.setupDevices();
  }

  async setupDevices() {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
      try {
        const stream = await navigator.mediaDevices.getUserMedia({
          video: true
        });
        if (stream) {
          this.video.nativeElement.srcObject = stream;
          this.video.nativeElement.play();
          this.error = null;
        } else {
          this.error = "You have no output video device";
        }
      } catch (e) {
        this.error = e;
      }
    }
  }

  capture() {
    this.drawImageToCanvas(this.video.nativeElement);
    this.captures.push(this.canvas.nativeElement.toDataURL("image/png"));
    this.isCaptured = true;
  }

  drawImageToCanvas(image: any) {
    this.canvas.nativeElement
      .getContext("2d")
      .drawImage(image, 0, 0, this.WIDTH, this.HEIGHT);
  }
}



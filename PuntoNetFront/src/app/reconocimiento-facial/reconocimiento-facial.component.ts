import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import {  Router } from '@angular/router';
import { WebcamImage, WebcamInitError, WebcamUtil } from 'ngx-webcam';
import { Subject, Observable } from 'rxjs';
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
  @Output() getPicture = new EventEmitter<WebcamImage>();
  errors: WebcamInitError[] = [];
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

  title = 'app';
  selectedFile = null;

  onFileSelected(event)
  {
    this.selectedFile = event.target.files[0];
  }

  onUpload()
  {
    console.log(this.selectedFile); // You can use FormData upload to backend server
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
    WebcamUtil.getAvailableVideoInputs()
      .then((mediaDevices: MediaDeviceInfo[]) => {
        this.isCameraExist = mediaDevices && mediaDevices.length > 0;
      });
  }

  takeSnapshot(): void {
    this.trigger.next();
  }

  onOffWebCame() {
    this.showWebcam = !this.showWebcam;
  }

  handleInitError(error: WebcamInitError) {
    this.errors.push(error);
  }

  changeWebCame(directionOrDeviceId: boolean | string) {
    this.nextWebcam.next(directionOrDeviceId);
  }

  handleImage(webcamImage: WebcamImage) {
    this.getPicture.emit(webcamImage);
    this.showWebcam = false;
  }

  get triggerObservable(): Observable<void> {
    return this.trigger.asObservable();
  }

  get nextWebcamObservable(): Observable<boolean | string> {
    return this.nextWebcam.asObservable();
  }
}



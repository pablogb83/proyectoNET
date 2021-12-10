import { Component, ElementRef, EventEmitter, Inject, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {  Router } from '@angular/router';
import { Subject } from 'rxjs';
import Swal from 'sweetalert2';
import { Data } from '../acceso/acceso-list/acceso-list.component';
import { AccesoService } from '../core/services/acceso.service';
import { FileService } from '../core/services/file.service';
import { HandleErrorsService } from '../core/services/handle.errors.service';
import { PersonaService } from '../core/services/persona.service';
import { PersonaAddComponent } from '../persona/persona-add/persona-add.component';

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
  noEncontrado = false;

  private trigger: Subject<void> = new Subject<void>();
  private nextWebcam: Subject<boolean | string> = new Subject<boolean | string>();
  constructor(public dialog: MatDialog,private fileService: FileService, public dialogRef: MatDialogRef<ReconocimientoFacialComponent>,@Inject(MAT_DIALOG_DATA) public data: Data, private service:AccesoService, private personaService:PersonaService, public router: Router,private handleError: HandleErrorsService,) { 
    this.idPuerta = data.idpuerta;
  }


  fileChangedHandler(imagen){
    
    const webcamImage = imagen;
    const arr = webcamImage.imageAsDataUrl.split(",");
    const mime = arr[0].match(/:(.*?);/)[1];
    const bstr = atob(arr[1]);
    let n = bstr.length;
    const u8arr = new Uint8Array(n);
    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }
    const file: File = new File([u8arr],"random")
    const formData:FormData=new FormData();
    formData.append('uploadedFile',file,"yoquese");
    this.fileService.UploadfileFace(formData).subscribe((data: any)=>{
      this.showSuccessAlert(data.nombres);
      this.PhotoFilePath = this.fileService.PhotoUrl + data.photoFileName;
      this.noEncontrado = false;
      this.agregarAcceso(data.id);
    },err=>{
      console.log(err);
      this.noEncontrado = true;
      this.handleError.showErrors(err)
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

  redireccionar():void{
    this.onNoClick();
      const dialogRef = this.dialog.open(PersonaAddComponent, {
        width: '500px',
      });
      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
        this.router.navigate(['/personas']);
      }); 
  }
}



import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import jsPDF from 'jspdf';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
pdfMake.vfs = pdfFonts.pdfMake.vfs;
import htmlToPdfmake from 'html-to-pdfmake';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-facturacion-detalle',
  templateUrl: './facturacion-detalle.component.html',
  styleUrls: ['./facturacion-detalle.component.css']
})
export class FacturacionDetalleComponent implements OnInit {

  title = 'factra';
  @ViewChild('factura') factura: ElementRef;
  constructor(public dialogRef: MatDialogRef<FacturacionDetalleComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {
    console.log(data);
   }

  ngOnInit(): void {
  }

  public downloadAsPDF() {
    let data = document.getElementById('factura');
    html2canvas(data,{
      scale: 2,
    }).then(canvas => {
          
      let docWidth = 208;
      let docHeight = canvas.height * docWidth / canvas.width;
      
      const contentDataURL = canvas.toDataURL('image/png')
      let doc = new jsPDF('p', 'mm', 'a4');
      let position = 0;
      doc.addImage(contentDataURL, 'PNG', 0, position, docWidth, docHeight)
      
      doc.save('exportedPdf.pdf');
  });
  }

}

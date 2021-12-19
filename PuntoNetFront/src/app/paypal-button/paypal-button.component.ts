import { AfterContentInit, AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { EmailService } from '../core/services/email.service';
import { InstitucionService } from '../core/services/institucion.service';
import { TokenStorageService } from '../core/services/token-storage.service';

declare var paypal: any;

@Component({
  selector: 'app-paypal-button',
  templateUrl: './paypal-button.component.html',
  styleUrls: ['./paypal-button.component.css']
})
export class PaypalButtonComponent implements AfterContentInit {
  @ViewChild('paypal', { static: true }) paypalElement!: ElementRef;


  constructor(private service: TokenStorageService, private institucionService: InstitucionService, private router: Router, private emailService: EmailService) {
   }
  ngAfterContentInit(): void {
    const tenant_id = this.service.getTenant();
    this.institucionService.getInstitucion().subscribe((institucionInfo: any)=>{
      console.log(institucionInfo);
      var subID ="";
      paypal.Buttons({
        style: {
            shape: 'rect',
            color: 'gold',
            layout: 'horizontal',
            label: 'paypal',
            tagline: 'false'
        },
        onClick: async (data,actions) =>{
          const status = await this.institucionService.isActive().toPromise();
          if(status){
            Swal.fire("Listo","Ya ha realizado el pago correspondiente en PayPal, si aun no nota cambios en el sistema por favor vuelva a iniciar sesion","info");
            return actions.reject();
          }
        },
        createSubscription: async (data: any, actions: any) => {
          return await actions.subscription.create({
            /* Creates the subscription */
            plan_id: institucionInfo.planId,
            custom_id: tenant_id
          });
        },
        onApprove: (data: any, actions: any) => {
          //this.emailService.sendEmail().subscribe();
          Swal.fire(
            'Pago completado',
            'El proceso de pago puede tomar algo de tiempo, recomendamos volver a iniciar sesion en la plataforma en unos minutos',
            'info'
          )
          this.service.signOut();
          this.router.navigate(['/']);
        },
        onCancel: (data:any)=>{
        },
        onError: (data: any) => {
        }
    }).render(this.paypalElement.nativeElement); 
    });
    
  }

  ngOnInit() {
   
  }
}

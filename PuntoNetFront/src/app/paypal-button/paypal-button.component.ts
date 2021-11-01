import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { window } from 'rxjs-compat/operator/window';
import { InstitucionService } from '../core/services/institucion.service';
import { TokenStorageService } from '../core/services/token-storage.service';

declare var paypal: any;

@Component({
  selector: 'app-paypal-button',
  templateUrl: './paypal-button.component.html',
  styleUrls: ['./paypal-button.component.css']
})
export class PaypalButtonComponent implements OnInit {
  @ViewChild('paypal', { static: true }) paypalElement!: ElementRef;
  constructor(private service: TokenStorageService, private institucionService: InstitucionService, private router: Router) { }


  ngOnInit() {
    const tenant_id = this.service.getTenant();
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
          console.log("EL STATUS ES: ", status);
          if(status){
            return actions.reject();
          }
        },
        createSubscription: async (data: any, actions: any) => {
          return await actions.subscription.create({
            /* Creates the subscription */
            plan_id: 'P-97818393X7850501NMFRB3JQ',
            custom_id: tenant_id
          });
        },
        onApprove: (data: any, actions: any) => {
          this.service.saveStatus(true);
          this.router.navigate(['/']);
        },
        onCancel: (data:any)=>{
        },
        onError: (data: any) => {
        }
    }).render(this.paypalElement.nativeElement); 

  }
}

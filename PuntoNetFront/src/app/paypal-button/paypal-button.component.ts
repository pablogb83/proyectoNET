import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TokenStorageService } from '../core/services/token-storage.service';

declare var paypal: any;

@Component({
  selector: 'app-paypal-button',
  templateUrl: './paypal-button.component.html',
  styleUrls: ['./paypal-button.component.css']
})
export class PaypalButtonComponent implements OnInit {
  @ViewChild('paypal', { static: true }) paypalElement!: ElementRef;
  constructor(private service: TokenStorageService) { }


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
        createSubscription: async (data: any, actions: any) => {
          return await actions.subscription.create({
            /* Creates the subscription */
            plan_id: 'P-97818393X7850501NMFRB3JQ',
            custom_id: tenant_id
          });
        },
        onApprove: function(data: any, actions: any) {
          alert(data.subscriptionID); // You can add optional success message for the subscriber here
        },
        onCancel: (data:any)=>{
          console.log("ERRORR: ", subID);
        },
        onError: (data: any) => {
        }
    }).render(this.paypalElement.nativeElement); 

  }
}

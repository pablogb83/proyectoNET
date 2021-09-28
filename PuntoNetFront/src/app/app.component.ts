import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenStorageService } from './Services/token-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'PuntoNetFront';

  isLoggedIn?=false;
  username : any ='';

  constructor(private tokenStorage: TokenStorageService, public router: Router) {}

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.username = this.tokenStorage.getUserName()
    }
  }

  logout(): void {
    //console.log(this.tokenStorage.getToken());
    this.tokenStorage.signOut();
    //console.log(this.tokenStorage.getToken());
    this.router.navigateByUrl('/').then(()=>{
      window.location.reload();
    });
  }

}

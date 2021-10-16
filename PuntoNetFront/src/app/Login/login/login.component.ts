import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/Services/login.service';
import { TokenStorageService } from 'src/app/Services/token-storage.service';




@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: any = {
    username: null,
    password: null
  };
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  users = [];

  constructor(
    private authService: LoginService, private tokenStorage: TokenStorageService, public router: Router) {}

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
    }
  }


  onSubmit(): void {
    const { username, password } = this.form;
    console.log(1);
    this.authService.login(username, password).subscribe(
      data => {
        this.tokenStorage.saveToken(data.token);
        this.tokenStorage.saveUserName(username);
        this.tokenStorage.saveRoleName(data.role);
        this.isLoggedIn=true;
        if(this.isLoggedIn){
          console.log('Logueado')
        }
         this.router.navigateByUrl('/').then(()=>{
           window.location.reload();
         });
      },
      err => {
        console.log(4);
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    );
  }

}

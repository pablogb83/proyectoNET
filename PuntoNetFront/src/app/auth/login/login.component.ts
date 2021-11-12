import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { EMPTY, of } from 'rxjs';
import 'rxjs/add/operator/delay';

import { AuthenticationService } from '../../core/services/auth.service';
import { NotificationService } from '../../core/services/notification.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { InstitucionService } from 'src/app/core/services/institucion.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    loginForm: FormGroup;
    loading: boolean;

    constructor(private router: Router,
        private titleService: Title,
        private notificationService: NotificationService,
        private authenticationService: AuthenticationService,
        private tokenService: TokenStorageService,
        private service: InstitucionService) {
    }

    ngOnInit() {
        this.titleService.setTitle('Proyecto .NET Login');
        //this.authenticationService.logout();
        this.createForm();
    }

    private createForm() {
        const savedUserEmail = localStorage.getItem('savedUserEmail');

        this.loginForm = new FormGroup({
            email: new FormControl(savedUserEmail, [Validators.required, Validators.email]),
            password: new FormControl('', Validators.required),
            rememberMe: new FormControl(savedUserEmail !== null)
        });
    }

    login() {
        const email = this.loginForm.get('email').value;
        const password = this.loginForm.get('password').value;
     //   const rememberMe = this.loginForm.get('rememberMe').value;

        this.loading = true;
        this.authenticationService.login(email.toLowerCase(), password).subscribe(data => {
            console.log(data);
            this.tokenService.saveToken(data.token);
            this.tokenService.saveUserName(data.email);
            this.tokenService.saveRoleName(data.role);
            this.tokenService.saveTenant(data.tenantId);
            this.tokenService.saveUserId(data.id);
            if(data.role!=="SUPERADMIN"){
                this.service.isActive().subscribe(status=>{
                    this.tokenService.saveStatus(Boolean(status))
                    this.loading = false;
                })
            }
            this.router.navigate(['/']);
        },
        error => {
            this.notificationService.openSnackBar(error.error);
            this.loading = false;
        });
    }

    resetPassword() {
        this.router.navigate(['/auth/password-reset-request']);
    }
}

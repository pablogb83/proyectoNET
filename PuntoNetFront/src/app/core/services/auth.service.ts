import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import * as moment from 'moment';
import 'rxjs/add/operator/delay';

import { Observable } from 'rxjs';

const AUTH_API = '/api/usuarios/authenticate/';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    constructor(private http: HttpClient,
        @Inject('LOCALSTORAGE') private localStorage: Storage) {
    }

    login(email: string, password: string): Observable<any> {
        return this.http.post(AUTH_API, {
          email,
          password,
          },httpOptions);
    }

    /*login(email: string, password: string) {
        return of(true).delay(1000)
            .pipe(map((/*response) => {
                // set token property
                // const decodedToken = jwt_decode(response['token']);

                // store email and jwt token in local storage to keep user logged in between page refreshes
                this.localStorage.setItem('currentUser', JSON.stringify({
                    token: 'aisdnaksjdn,axmnczm',
                    isAdmin: true,
                    email: 'john.doe@gmail.com',
                    id: '12312323232',
                    alias: 'john.doe@gmail.com'.split('@')[0],
                    expiration: moment().add(1, 'days').toDate(),
                    fullName: 'John Doe'
                }));

                return true;
            }));
    }*/

    logout(): void {
        // clear token remove user from local storage to log user out
        this.localStorage.removeItem('currentUser');
    }

    getCurrentUser(): any {
        // TODO: Enable after implementation
        // return JSON.parse(this.localStorage.getItem('currentUser'));
        return {
            token: 'aisdnaksjdn,axmnczm',
            isAdmin: true,
            email: 'john.doe@gmail.com',
            id: '12312323232',
            alias: 'john.doe@gmail.com'.split('@')[0],
            expiration: moment().add(1, 'days').toDate(),
            fullName: 'John Doe'
        };
    }

}

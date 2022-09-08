import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http' 
import { Observable } from 'rxjs';
import { UserLogin } from '../models/userLogin';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  URI = environment.apiEndpoint + '/login'
  constructor(private http: HttpClient) { }

  logIn(user: UserLogin): Observable<any> {
    return this.http.post(this.URI, user); 
  }
  setTokenLocalStorage(token: string):void {
    localStorage.setItem('token', token);
    var tokenDecoded = this.getTokenDecoded();
    localStorage.setItem('username', tokenDecoded.username);
  }
  setUserIdLocalStorage(userId: number): void {
    localStorage.setItem('userId',userId.toString());
  }
  getUserIdLocalStorage(): number {
    return Number(localStorage.getItem('userId'));
  }
  getUsernameLocalStorage(): string | null {
    return localStorage.getItem('username');
  }
  getTokenLocalStorage(): string | null{
    return localStorage.getItem('token');
  }
  private getTokenDecoded(): any{
    const helper = new JwtHelperService();
    const token = localStorage.getItem('token');
    if (token) {
      return helper.decodeToken(token);
    }
    return null;
  }

  removeLocalStorage(): void {
    localStorage.removeItem('username');
    localStorage.removeItem('userId');
    localStorage.removeItem('token');
  }
  
}

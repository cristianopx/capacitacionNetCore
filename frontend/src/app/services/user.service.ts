import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { UserLogin } from '../models/userLogin';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  URL: string = environment.apiEndpoint+'/user';

  constructor(private http: HttpClient) { }

  getUser(username:string): Observable<User>{
    return this.http.get(this.URL+`/${username}`);
  }

  saveUser(user: UserLogin): Observable<any>{
    return this.http.post(this.URL, user);
  }
  
  changePassword(changePassword: any): Observable<any> {
    return this.http.put(this.URL+`/changePassword`,changePassword)
  }
}

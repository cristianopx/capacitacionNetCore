import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {
  user?: User;
  constructor(private loginService: LoginService, private userService:UserService, private router:Router) {
    const username = this.loginService.getUsernameLocalStorage();
    if(username){
      this.getUser(username);
    }else{
      this.router.navigate(['/inicio/login']);
    }
  }

  ngOnInit(): void {

  }

  getUser(username: string): void {
    this.userService.getUser(username).subscribe({
      next: res => {
        this.user = res;
        if(this.user.id){
          this.loginService.setUserIdLocalStorage(this.user.id);
        }
    },error: err => {
        this.router.navigate(['/inicio/login']);
    }});
  }

}

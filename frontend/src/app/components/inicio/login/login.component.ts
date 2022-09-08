import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserLogin } from 'src/app/models/userLogin';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login:FormGroup;
  loading: boolean = false;

  constructor(private fb: FormBuilder, 
    private toastr: ToastrService, 
    private router: Router,
    private loginService: LoginService) {
    this.login = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  ngOnInit(): void {
  }

  logIn(): void {
    const user: UserLogin = {
      username: this.login.value.username,
      password: this.login.value.password
    }
    this.loading = true;
    this.loginService.logIn(user).subscribe({
      next : (res) => {
        this.toastr.success(`Bienvenido`);
        this.loginService.setTokenLocalStorage(res.token);
        this.loading = false;
        this.login.reset();
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.toastr.error(err.error.message,'UPS!')
        console.log(err);
        
        this.loading = false;
      }
    });
  }

  

}

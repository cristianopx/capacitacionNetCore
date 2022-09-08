import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserLogin } from 'src/app/models/userLogin';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  register:FormGroup;
  loading:boolean = false;

  constructor(private fb:FormBuilder, 
    private userService: UserService, 
    private router: Router,
    private toast: ToastrService) { 
    this.register = this.fb.group({
      username:['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4)]],
      confirmPassword:['']
    },
    {
      validators: this.checkPassword
    }
    )
  }

  ngOnInit(): void {
  }

  registrarUsuario(): void{
    console.log(this.register);
    const user: UserLogin = {
      username: this.register.value.username,
      password: this.register.value.password
    };
    this.loading = true
    this.userService.saveUser(user).subscribe({
      next: res => {
        console.log(res);
        this.toast.success(`Usuario ${res.username} creado`, res.message)
        this.loading = false;
        this.router.navigate(['/inicio/login']);
      },
      error: err => {
        console.log(err.error);
        this.toast.error(err.error.message, 'UPS!');
        this.loading = false;
      }
    })
      
  }

  checkPassword(group: FormGroup): any {
    return group.value.password === group.value.confirmPassword ? null : { notSame: true};
  }

}

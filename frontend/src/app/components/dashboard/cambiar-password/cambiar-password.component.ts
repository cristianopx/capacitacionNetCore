import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-cambiar-password',
  templateUrl: './cambiar-password.component.html',
  styleUrls: ['./cambiar-password.component.css']
})
export class CambiarPasswordComponent implements OnInit {
  changePassword: FormGroup;
  loading: boolean = false;

  constructor(private fb: FormBuilder, private userService: UserService, private loginService: LoginService,private toastr: ToastrService, private router: Router) {
    this.changePassword = this.fb.group({
      oldPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(4)]],
      confirmPassword: ['']
    },
    {
      validators: this.checkPassword
    }
    );
   }

  ngOnInit(): void {
  }
  checkPassword(group: FormGroup): any {
    return group.value.newPassword === group.value.confirmPassword ? null : { notSame: true};
  }

  savePassword(): void{
    this.loading = true;
    const passwordToChange = {
      oldPassword: this.changePassword.value.oldPassword,
      newPassword: this.changePassword.value.newPassword
    }
    this.userService.changePassword(passwordToChange).subscribe({
      next: (res) => {
        this.toastr.info(res.message)
        this.loading = false;
        this.router.navigate(['/dashboard'])
      },
      error: (err) => {
        this.toastr.error(err.error.message, 'Ups!')
        this.loading = false;
      }
    });
    
  }

}

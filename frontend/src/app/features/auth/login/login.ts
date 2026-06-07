import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
})
export class Login {
  constructor(private router : Router, private authService : AuthService){}

  email : string = '';
  password : string = '';

  login(){
    this.authService.login({ email: this.email, password: this.password})
      .subscribe(() => this.router.navigate(['/tasks']))
  }
}

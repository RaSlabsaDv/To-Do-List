import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
})
export class Register {
  constructor(private router : Router, private authService : AuthService ){}

  username : string = '';
  email : string = '';
  password : string = '';

  register(){
    this.authService.register({ name: this.username, email: this.email, password: this.password })
      .subscribe(() =>{
        this.authService.login({ email: this.email, password: this.password }).subscribe(() =>{
          this.router.navigate(['/tasks'])
        })
      })
  }
}

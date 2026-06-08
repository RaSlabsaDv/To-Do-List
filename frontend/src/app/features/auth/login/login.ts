import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';


@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
})
export class Login {
  constructor(private router : Router, private authService : AuthService){}

  private fb = inject(FormBuilder);

  submitted = false;
  errorMessage = signal('');

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  })

  login(){
    this.submitted = true;
    if(this.form.invalid) return;

    const { email, password } = this.form.value;
    
    this.authService.login({ email: email!, password: password!}).subscribe({
      next: () => this.router.navigate(['/tasks']),
      error: () => this.errorMessage.set('Невірний пошта або пароль')
    });
  }
}

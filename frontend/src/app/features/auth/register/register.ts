import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './register.html',
})
export class Register {
  constructor
  (
    private router : Router, 
    private authService : AuthService,
  ){}

  private fb = inject(FormBuilder);

  submitted = false;
  errorMessage = signal('');

  form = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(1)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  })

  register(){
    this.submitted = true;
    if (this.form.invalid) return;

    const { name, email, password } = this.form.value;

    this.authService.register({ name: name!, email: email!, password: password! })
      .subscribe({
        next: () => {
          this.authService.login({ email: email!, password: password! }).subscribe(() =>{
            this.router.navigate(['/tasks'])
          })
        },
        error: (err) => {
          if (err.status === 409) this.errorMessage.set('Користувач з таким email вже існує');
          else this.errorMessage.set('Щось пішло не так');
        }
      })
  }
}

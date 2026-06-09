// navbar.ts
import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { UserService } from '../../../core/services/user.service';
import { DrawerComponent } from '../drawer/drawer.component';
import { User } from '../../../core/models/user.model';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [DrawerComponent, ReactiveFormsModule],
  templateUrl: './navbar.html'
})
export class Navbar {
  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) {}

  private fb = inject(FormBuilder)

  form = this.fb.group({
    name: ['', [Validators.minLength(2)]],
    email: ['', [Validators.email]],
    password: ['', [Validators.minLength(6)]]
  })

  showProfileDrawer = false;
  user = signal<User | null>(null);

  ngOnInit() {
    const currentUrl = this.router.url;
    if (currentUrl === '/login' || currentUrl === '/register' || currentUrl === '/') return;

    this.userService.getById().subscribe(u => {
      this.user.set(u);
      this.form.patchValue({ name: u.name });
      this.form.patchValue({ email: u.email });
    });
  }

  openProfile() {
    this.showProfileDrawer = true;
    this.userService.getById().subscribe(u => {
      this.user.set(u);
      this.form.patchValue({ name: u.name });
      this.form.patchValue({ email: u.email });
    });
  }

  logout() {
    this.showProfileDrawer = false;
    this.authService.logout().subscribe(() => {
      this.router.navigate(['/login']);
    });
  }

  saveProfile() {
    if(this.form.invalid) return;

    const { name, email, password } = this.form.value

    this.showProfileDrawer = false;
    this.userService.update({
      name: name || undefined,
      email: email || undefined,
      password: password || undefined
    }).subscribe()
  }

  deleteAccount() {
    this.showProfileDrawer = false;
    this.userService.delete().subscribe(() => {
      this.authService.logout().subscribe(() => {
        this.router.navigate(['/login']);
      });
    });
  }
}
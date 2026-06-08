// navbar.ts
import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { UserService } from '../../../core/services/user.service';
import { DrawerComponent } from '../drawer/drawer.component';
import { User } from '../../../core/models/user.model';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [DrawerComponent, FormsModule],
  templateUrl: './navbar.html'
})
export class Navbar {
  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) {}

  showProfileDrawer = false;
  user = signal<User | null>(null);
  name = signal<string>('');
  email = '';
  password = '';

  ngOnInit() {
  const currentUrl = this.router.url;
  if (currentUrl === '/login' || currentUrl === '/register' || currentUrl === '/') return;

  this.userService.getById().subscribe(u => {
    this.user.set(u);
    this.name.set(u.name);
    this.email = u.email;
  });
}
  openProfile() {
  this.showProfileDrawer = true;
  this.userService.getById().subscribe(u => {
    this.user.set(u);
    this.name.set(u.name);
    this.email = u.email;
  });
}

  logout() {
    this.showProfileDrawer = false;
    this.authService.logout().subscribe(() => {
      this.router.navigate(['/login']);
    });
  }

  saveProfile() {
    this.showProfileDrawer = false;
    this.userService.update({
      name: this.name() || undefined,
      email: this.email || undefined,
      password: this.password || undefined
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
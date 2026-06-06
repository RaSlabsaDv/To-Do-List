import { Routes } from '@angular/router';
import { Register } from './features/auth/register/register';
import { Login } from './features/auth/login/login';
import { TaskList } from './features/tasks/task-list/task-list';
import { authGuard } from './core/guards/auth.guard';
import { NotFound } from './features/not-found/not-found';
import { Preview } from './features/preview/preview';

export const routes: Routes = [
    { path: '', component: Preview},
    { path: 'register', component: Register },
    { path: 'login', component: Login },
    { path: 'tasks', component: TaskList, canActivate: [authGuard] },
    { path: '**', component: NotFound}
];

import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "../services/auth.service";
import { catchError, map, of } from "rxjs";


export const authGuard : CanActivateFn = () =>{
    const authService = inject(AuthService);
    const route = inject(Router);

    return authService.checkAuth().pipe(
        map(() => true),
        catchError(() =>{
            route.navigate(['/login'])
            return of(false)
        })
    )
}
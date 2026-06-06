import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateUserDto, LoginDto, User} from "../models/user.model";

@Injectable({providedIn: "root"})
export class AuthService{
    private api = 'http://localhost:5284/api/auth'

    constructor(private http : HttpClient){}

    register(dto : CreateUserDto) : Observable<void>{
        return this.http.post<void>(`${this.api}/register`, dto);
    }

    login(dto : LoginDto) : Observable<void>{
        return this.http.post<void>(`${this.api}/login`, dto)
    }

    logout() : Observable<void>{
        return this.http.post<void>(`${this.api}/logout`, {});
    }

    checkAuth() : Observable<User>{
        return this.http.get<User>(`${this.api}/me`);
    }
}
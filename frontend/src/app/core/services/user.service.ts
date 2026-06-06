import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UpdateUserDto, User } from "../models/user.model";

@Injectable({providedIn: 'root'})
export class UserService{
    private api = 'http://localhost:5284/api/user'

    constructor(private http : HttpClient){}

    getById() : Observable<User>{
        return this.http.get<User>(this.api);
    }

    update(dto : UpdateUserDto) : Observable<void>{
        return this.http.put<void>(this.api, dto);
    }

    delete() : Observable<void>{
        return this.http.delete<void>(this.api);
    }
}
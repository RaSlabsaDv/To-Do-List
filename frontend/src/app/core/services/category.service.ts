import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreateCategoryDto, UpdateCategoryDto, Category } from "../models/category.model";
import { Observable } from "rxjs";

@Injectable({providedIn: 'root'})
export class CategoryService{
    private api = 'http://localhost:5284/api/category'

    constructor(private http : HttpClient){}

    create(dto : CreateCategoryDto) : Observable<void>{
        return this.http.post<void>(this.api, dto);
    }

    getById(id : number) : Observable<Category>{
        return this.http.get<Category>(`${this.api}/${id}`);
    }

    getByUser() : Observable<Category[]>{
        return this.http.get<Category[]>(`${this.api}/user`);
    }

    update(id : number, dto : UpdateCategoryDto) : Observable<void>{
        return this.http.put<void>(`${this.api}/${id}`, dto);
    }

    delete(id : number) : Observable<void>{
        return this.http.delete<void>(`${this.api}/${id}`);
    }
}
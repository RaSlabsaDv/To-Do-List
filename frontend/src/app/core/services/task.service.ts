import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreateTaskDto, Task, UpdateTaskDto } from "../models/task.model";

@Injectable({providedIn: "root"})
export class TaskService{
    private api = 'http://localhost:5284/api/usertask'

    constructor(private http: HttpClient) {}

    create(dto : CreateTaskDto) : Observable<void>{
        return this.http.post<void>(this.api, dto);
    }

    getById(id : number) : Observable<Task>{
        return this.http.get<Task>(`${this.api}/${id}`);
    }

    getByUser() : Observable<Task[]>{
        return this.http.get<Task[]>(`${this.api}/user`);
    }

    update(id : number, dto : UpdateTaskDto) : Observable<void>{
        return this.http.put<void>(`${this.api}/${id}`, dto);
    }

    delete(id : number) : Observable<void>{
        return this.http.delete<void>(`${this.api}/${id}`);
    }

    complete(id : number) : Observable<void>{
        return this.http.patch<void>(`${this.api}/${id}/complete`, {});
    }
}
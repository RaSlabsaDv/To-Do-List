import { Category } from "./category.model";

export interface Task{
    id: number,
    label: string,
    description? : string,
    deadline? : string,
    reminder? : string,
    repeatState : RepeatState,
    isCompleted : boolean,
    category?: Category;
}

export interface CreateTaskDto{
    label: string,
    description? : string,
    deadline? : string,
    reminder? : string,
    repeatState : RepeatState,
    categoryId? : number
}

export interface UpdateTaskDto{
    label?: string,
    description? : string,
    deadline? : string,
    reminder? : string,
    repeatState? : RepeatState,
    categoryId? : number
}

export enum RepeatState{
    NoRepeat = 0,
    EveryDay = 1,
    OnWorkDays = 2,
    EveryWeek = 3,
    EveryMonth = 4,
    EveryYear = 5
}
import { Component, Input, Output, EventEmitter, input } from '@angular/core';
import { Task } from '../../../core/models/task.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-task-card',
  imports: [DatePipe],
  templateUrl: './task-card.html',
})
export class TaskCard {
  @Input() task! : Task;
  
  @Output() complete = new EventEmitter<void>();
  @Output() edit = new EventEmitter<void>(); 
  @Output() delete = new EventEmitter<void>();
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CreateTaskDto, RepeatState, Task, UpdateTaskDto } from '../../../core/models/task.model';
import { FormsModule } from '@angular/forms';
import { Category } from '../../../core/models/category.model';

@Component({
  selector: 'app-task-form',
  imports: [FormsModule],
  templateUrl: './task-form.html',
})
export class TaskForm {
  @Input() task! : Task;
  @Input() categories : Category[] = [];

  @Output() create = new EventEmitter<CreateTaskDto>()
  @Output() save = new EventEmitter<UpdateTaskDto>();
  @Output() cancel = new EventEmitter<void>();

  label : string = '';
  description? : string;
  deadline? : string;
  reminder? : string;
  repeatState? : RepeatState;
  category? : Category;
  categoryId : number | null = this.category?.id ?? null;

  ngOnInit() {
    if (this.task) {
      this.label = this.task.label;
      this.description = this.task.description;
      this.deadline = this.task.deadline;
      this.reminder = this.task.reminder;
      this.repeatState = this.task.repeatState;
      this.category = this.task.category;
      this.category?.id ?? undefined;
    }
  }

  onSave() {
     console.log('categoryId type', typeof this.categoryId);
    if (this.task) {
      this.save.emit({
        label: this.label,
        description: this.description,
        deadline: this.deadline,
        reminder: this.reminder,
        repeatState: this.repeatState ? +this.repeatState : RepeatState.NoRepeat,
        categoryId: this.categoryId ?? undefined
      });
    } else {
      this.create.emit({
        label: this.label,
        description: this.description,
        deadline: this.deadline,
        reminder: this.reminder,
        repeatState: this.repeatState ? +this.repeatState : RepeatState.NoRepeat,
        categoryId: this.categoryId ?? undefined
      });
    }
  }
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Category } from '../../../core/models/category.model';

@Component({
  selector: 'app-task-filter',
  imports: [],
  templateUrl: './task-filter.html',
})
export class TaskFilter {
  @Input() categories : Category[] = [];
  @Input() selectedCategoryId : number | null = null; 

  @Output() categorySelected = new EventEmitter<number | null>();
  @Output() createCategory = new EventEmitter<void>();
  @Output() editCategory = new EventEmitter<Category>();
  @Output() deleteCategory = new EventEmitter<number>();
}

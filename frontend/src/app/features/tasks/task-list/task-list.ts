import { Component, computed, OnInit, signal } from '@angular/core';
import { TaskCard } from '../task-card/task-card';
import { CreateTaskDto, Task, UpdateTaskDto } from '../../../core/models/task.model';
import { TaskService } from '../../../core/services/task.service';
import { ModalComponent } from '../../../shared/components/modal/modal.component';
import { DrawerComponent } from '../../../shared/components/drawer/drawer.component';
import { TaskForm } from '../task-form/task-form';
import { TaskFilter } from '../task-filter/task-filter';
import { Category, CreateCategoryDto } from '../../../core/models/category.model';
import { CategoryService } from '../../../core/services/category.service';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-task-list',
  imports: [TaskCard, ModalComponent, DrawerComponent, TaskForm, TaskFilter, FormsModule],
  templateUrl: './task-list.html',
})
export class TaskList{
  constructor(private taskService : TaskService, private categoryService : CategoryService){}

  tasks = signal<Task[]>([]);

  currentPage = signal<number>(1);
  pageSize = 5;

  filteredTasks = computed(() => {
    const catId = this.selectedCategoryId();
    const query = this.searchQuery().toLowerCase();
    
    return this.tasks()
      .filter(t => !catId || t.category?.id === catId)
      .filter(t => !query || t.label.toLowerCase().includes(query));
  });

  totalPages = computed(() =>{
    return Math.ceil(this.filteredTasks().length / this.pageSize);
  });

  pages = computed(() => 
    Array.from({ length: this.totalPages() }, (_, i) => i + 1)
  );

  paginatedTasks = computed(() =>{
    const start = (this.currentPage() - 1) * this.pageSize;
    return this.filteredTasks().slice(start, start + this.pageSize);
  });

  categories = signal<Category[]>([])
  selectedCategoryId = signal<number | null>(null);

  selectedCategoryForAction: number | null = null;
  showCreateCategoryModal = false;
  newCategoryName : string = '';

  showEditCategoryModal = false;
  selectedCategory : Category | null = null;
  editCategoryName : string = ''

  showDeleteCategoryModal = false;

  showCreateDrawer = false;
  
  showEditDrawer = false;
  selectedTask : Task | null = null;

  showDeleteModal = false;
  selectedTaskId : number | null = null;

  searchQuery = signal<string>('');
  

  ngOnInit() {
    this.taskService.getByUser().subscribe(tasks => {
      this.tasks.set(tasks);
    });

    this.categoryService.getByUser().subscribe(cats =>{
      this.categories.set(cats);
    });
  }

  onComplete(taskId : number, isCompleted : boolean){
    if(isCompleted){
      this.taskService.uncomplete(taskId).subscribe(() => {
        this.tasks.update(tasks => tasks.map(t => t.id == taskId ? { ...t, isCompleted: false } : t))
      })
    }else{
      this.taskService.complete(taskId).subscribe(() => {
        this.tasks.update(tasks => tasks.map(t => t.id == taskId ? { ...t, isCompleted: true } : t))
      })
    }
  }

  confirmCreate(dto: CreateTaskDto) {
    this.taskService.create(dto).subscribe(() => {
      this.taskService.getByUser().subscribe(tasks => {
        this.tasks.set(tasks);
        this.showCreateDrawer = false;
      });
    });
  }

  onEdit(task : Task){
    this.showEditDrawer = true;
    this.selectedTask = task;
  }

  confirmEdit(dto: UpdateTaskDto) {
    if (!this.selectedTask) return;

    this.taskService.update(this.selectedTask.id, dto).subscribe({
      next: () => {
        this.taskService.getByUser().subscribe(tasks => {
          this.tasks.set(tasks);
          this.showEditDrawer = false;
          this.selectedTask = null;
        });
      }
    });
  }

  onDelete(taskId : number){
    this.showDeleteModal = true;
    this.selectedTaskId = taskId;
  }

  confirmDelete(){
    if(!this.selectedTaskId) return;

    this.taskService.delete(this.selectedTaskId).subscribe(() => {
      this.tasks.update(tasks => tasks.filter(t => t.id !== this.selectedTaskId));
      this.showDeleteModal = false;
      this.selectedTaskId = null;
    });
  }

  onCreateCategory(){
    this.showCreateCategoryModal = true;
  }

  confirmCreateCategory(){
    if(!this.newCategoryName.trim()) return;

    this.categoryService.create({ name: this.newCategoryName }).subscribe(() => {
      this.categoryService.getByUser().subscribe(cats => {
        this.categories.set(cats)
      });
      this.showCreateCategoryModal = false;
      this.newCategoryName = '';  
    });
  }

  onEditCategory(category: Category) {
    this.selectedCategory = category;
    this.selectedCategoryForAction = category.id;  // ← додати
    this.editCategoryName = category.name;          // ← додати
    this.showEditCategoryModal = true;
  }

  confirmEditCategory(){
    if(!this.editCategoryName.trim() || !this.selectedCategoryForAction) return;

    this.categoryService.update(this.selectedCategoryForAction, { name: this.editCategoryName }).subscribe(() =>{
      this.categoryService.getByUser().subscribe(cats => {
        this.categories.set(cats);
      });
      this.taskService.getByUser().subscribe(tasks => {
        this.tasks.set(tasks);
      })
      this.showEditCategoryModal = false;
      this.newCategoryName = '';
    });
  }

  onDeleteCategory(categoryId: number) {
    this.selectedCategoryForAction = categoryId;
    this.showDeleteCategoryModal = true;
  }

  confirmDeleteCategory(){
    if(!this.selectedCategoryForAction) return;

    this.categoryService.delete(this.selectedCategoryForAction).subscribe(() =>{
      this.categoryService.getByUser().subscribe(cats => {
        this.categories.set(cats);
      });
      this.taskService.getByUser().subscribe(tasks => {
        this.tasks.set(tasks);
      });
      this.showDeleteCategoryModal = false;
      this.selectedCategoryForAction = null; 
    });
  }
}

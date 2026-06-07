import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-modal',
  template: `
    <div class="fixed inset-0 bg-black/60 flex items-center justify-center z-50" (click)="close.emit()">
      <div class="bg-[#111a14] border border-[#1a2e22] rounded-xl p-6 w-full max-w-md" (click)="$event.stopPropagation()">
        <ng-content />
      </div>
    </div>
  `
})

export class ModalComponent {
  @Output() close = new EventEmitter<void>();
}
import { Component, EventEmitter, Output } from "@angular/core";

@Component({
  selector: 'app-drawer',
  template: `
    <div class="fixed inset-0 bg-black/40 z-40" (click)="close.emit()"></div>
    <div class="fixed right-0 top-0 h-full w-96 bg-[#111a14] border-l border-[#1a2e22] z-50 p-6 flex flex-col gap-4">
      <ng-content />
    </div>
  `
})

export class DrawerComponent {
  @Output() close = new EventEmitter<void>();
}

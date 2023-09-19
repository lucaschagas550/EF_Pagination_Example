import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-show-validation-errors',
  templateUrl: './show-validation-errors.component.html',
  styleUrls: ['./show-validation-errors.component.css']
})
export class ShowValidationErrorsComponent {

  @Input()
  errors !: string[];

  @Output()
  clearErrors: EventEmitter<string[]> = new EventEmitter<string[]>();

  closeError() {
    this.errors = [];
    this.clearErrorsEvent();
  }

  //Emite o evento para o componente pai com dados de erros limpos
  clearErrorsEvent(): void {
    this.clearErrors.emit(this.errors);
  }
}
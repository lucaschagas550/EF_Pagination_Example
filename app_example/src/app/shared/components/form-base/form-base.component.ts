import { ElementRef } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { Observable, fromEvent, merge } from "rxjs";
import { DisplayMessage, GenericValidator, ValidationMessages } from "../../utils/generic-form-validation";

export abstract class FormBaseComponent {
  //validadores do formulario generico e mensagens de aviso
  displayMessage: DisplayMessage = {};
  genericValidator!: GenericValidator;
  validationMessages!: ValidationMessages;
  unsavedChanges!: boolean;

  protected configureBaseValidationMessages(validationMessages: ValidationMessages): void {
    this.genericValidator = new GenericValidator(validationMessages);
  }

  protected configureBaseFormValidation(formInputElements: ElementRef[], formGroup: FormGroup): void {
    let controlBlurs: Observable<any>[] = formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')); //Blur sempre que tira o foco de algum elemento do form dispara um evento

    merge(...controlBlurs).subscribe(() => { //para cada evento(controlBlurs) sera processado uma mensagem
      this.validateForm(formGroup)
    });
  }

  protected validateForm(formGroup: FormGroup): void {
    this.displayMessage = this.genericValidator.processMessages(formGroup);
    console.log('formBaseComponent displaymessage => ', this.displayMessage);
    this.unsavedChanges = true;  //Utilizado no guard para avisar  que ira perder os dados da alteracao ao sair da tela
  }

  protected validateSubmitForm(formGroup: FormGroup): void {
    this.displayMessage = this.genericValidator.processSubmitMessages(formGroup);
    this.unsavedChanges = true; //Utilizado no guard para avisar o usuario que ira perder os dados da alteracao ao sair da tela
  }
}

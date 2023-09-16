import { FormGroup } from '@angular/forms';

export class GenericValidator {
    constructor(private validationMessages: ValidationMessages) { }

    processMessages(container: FormGroup): { [key: string]: number } {
        let messages: any = {};
        for (let controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                let c = container.controls[controlKey];

                if (c instanceof FormGroup) {
                    let childMessages = this.processMessages(c);
                    Object.assign(messages, childMessages);
                } else {
                    if (this.validationMessages[controlKey]) {
                        messages[controlKey] = '';
                        if ((c.dirty || c.touched) && c.errors) {
                            Object.keys(c.errors).map(messageKey => {
                                if (this.validationMessages[controlKey][messageKey]) {
                                    messages[controlKey] += this.validationMessages[controlKey][messageKey] + '<br />';
                                }
                            });
                        }
                    }
                }
            }
        }
        return messages;
    }

    //Para caso submeter sem preencher campos, exibir erros
    processSubmitMessages(container: FormGroup): { [key: string]: number } {
        let messages: any = {};
        for (let controlKey in container.controls) {
            if (container.controls.hasOwnProperty(controlKey)) {
                let c = container.controls[controlKey];

                if (c instanceof FormGroup) {
                    let childMessages = this.processSubmitMessages(c);
                    Object.assign(messages, childMessages);
                } else {
                    if (this.validationMessages[controlKey]) {
                        messages[controlKey] = '';
                        if (c.errors) {
                            Object.keys(c.errors).map(messageKey => {
                                if (this.validationMessages[controlKey][messageKey]) {
                                    messages[controlKey] += this.validationMessages[controlKey][messageKey] + '<br />';
                                }
                            });
                        }
                    }
                }
            }
        }
        return messages;
    }
}

export interface DisplayMessage {
    [key: string]: number
}
export interface ValidationMessages {
    [key: string]: { [key: string]: string }
}

import { FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material';

export class ErrorMatcher implements ErrorStateMatcher {
    formSubmitted = false;
    submitForm() {
        this.formSubmitted = true;
    }
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        return this.formSubmitted && control && control.invalid;
    }
}

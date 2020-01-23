import {
  Component, OnInit, Input, EventEmitter, Output, ViewChild, ElementRef, Self, Optional
} from '@angular/core';
import {
  ControlContainer, NgForm, ControlValueAccessor,
  Validator, AbstractControl,
  ValidationErrors, ValidatorFn, Validators, NgControl
} from '@angular/forms';

@Component({
  selector: 'app-text-box',
  templateUrl: './text-box.component.html',
  viewProviders: [{ provide: ControlContainer, useExisting: NgForm }],
  providers: []
})
export class TextBoxComponent implements OnInit, ControlValueAccessor, Validator {

  @ViewChild('input', { read: ElementRef, static: false }) input: ElementRef;
  @ViewChild('select', { read: ElementRef, static: false }) select: ElementRef;
  disabled;

  @Input() isRequired: boolean;
  @Input() placeholder: string;
  @Input() model: string;
  @Input() requiredErrorTxt: string;
  @Input() patternErrorTxt: string;
  @Input() isSubmitPressed: boolean;
  @Input() isDisabled = false;
  @Input() inputType: string;
  @Input() pattern: string;
  @Input() inputLength: number;
  @Output() textValue: EventEmitter<any> = new EventEmitter();
  control: any;

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  validate(c: AbstractControl): ValidationErrors {
    const validators: ValidatorFn[] = [];
    if (this.isRequired) {
      validators.push(Validators.required);
    }
    if (this.pattern) {
      validators.push(Validators.pattern(this.pattern));
    }

    return validators;
  }

  constructor(
    @Optional()
    @Self()
    public controlDir: NgControl
  ) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit() {
    const control = this.controlDir.control;
    const validators: ValidatorFn[] = control.validator ? [control.validator] : [];
    if (this.isRequired) {
      validators.push(Validators.required);
    }
    if (this.pattern) {
      validators.push(Validators.pattern(this.pattern));
    }
    control.setValidators(validators);
    control.updateValueAndValidity();
  }

  onChange(value: string) {
  }

  modelChange() {
  }


  onTouched() { }

  onSelectChange() {
  }

}

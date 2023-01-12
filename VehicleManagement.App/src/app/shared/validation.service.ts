import { Injectable } from '@angular/core';
import {
  AbstractControl,
  UntypedFormArray,
  UntypedFormControl,
  UntypedFormGroup,
} from '@angular/forms';

@Injectable({ providedIn: 'root' })
export class ValidationService {
  markFormControlsAsTouched(form: UntypedFormGroup): void {
    Object.keys(form.controls).forEach((field: string) => {
      const control: AbstractControl = form.get(field);

      if (control instanceof UntypedFormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof UntypedFormGroup) {
        this.markFormControlsAsTouched(control);
      } else if (control instanceof UntypedFormArray) {
        for (let i: number = 0; i < control.length; i++) {
          this.markFormControlsAsTouched(<UntypedFormGroup>control.controls[i]);
        }

        control.markAsTouched({ onlySelf: true });
      }
    });
  }
}

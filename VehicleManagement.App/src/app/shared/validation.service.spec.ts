import { ValidationService } from './validation.service';
import { TestBed } from '@angular/core/testing';
import { FormControl, FormGroup } from '@angular/forms';

describe('ValidationService', () => {
  let validationService: ValidationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});

    validationService = TestBed.inject(ValidationService);
  });

  it('touches form controls', () => {
    // ARRANGE
    const form: FormGroup = new FormGroup({
      control: new FormControl(),
    });

    // ACT
    validationService.markFormControlsAsTouched(form);

    // ASSERT
    expect(form.get('control').touched).toBe(true);
  });

  it('touches controls in nested form groups', () => {
    // ARRANGE
    const form: FormGroup = new FormGroup({
      group: new FormGroup({
        control: new FormControl(),
      }),
    });

    // ACT
    validationService.markFormControlsAsTouched(form);

    // ASSERT
    expect(form.get(['group', 'control']).touched).toBe(true);
  });
});

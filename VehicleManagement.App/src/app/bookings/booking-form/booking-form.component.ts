import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnDestroy,
} from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FlatVehicle } from 'src/app/vehicles/models/flat-vehicle.model';
import { VehicleFacade } from 'src/app/vehicles/_store/vehicle.facade';
import { ValidationService } from 'src/app/shared/validation.service';
import { Booking } from '../models/booking.model';

type formValue = {
  range: {
    start: Date;
    end: Date;
  };
  employeeNumber: string;
  fin: string;
};

@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.scss'],
})
export class BookingFormComponent implements OnInit, OnDestroy {
  @Input() edit: boolean = false;
  @Input() booking: Observable<Booking> = null;

  @Output() submitted: EventEmitter<Booking>;

  subscription: Subscription;
  bookingForm: FormGroup;

  get vehicles$(): Observable<FlatVehicle[]> {
    return this.vehicleFacade.vehicles$;
  }

  constructor(
    private vehicleFacade: VehicleFacade,
    private validationService: ValidationService
  ) {
    this.submitted = new EventEmitter<Booking>();
  }

  ngOnInit(): void {
    this.initBookingForm();
    this.vehicleFacade.getVehicles();

    this.subscription = this.booking?.subscribe((booking: Booking) => {
      this.setBookingFormValue(booking);
    });
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  isError(field: string, error: string): boolean {
    return (
      this.bookingForm?.get(field)?.errors != null &&
      this.bookingForm?.get(field)?.errors[error] &&
      this.bookingForm?.get(field)?.touched
    );
  }

  bookingSubmited(): void {
    if (this.bookingForm.valid) {
      const booking: Booking = {
        start: this.bookingForm.value.range.start,
        end: this.bookingForm.value.range.end,
        employeeNumber: this.bookingForm.value.employeeNumber,
        fin: this.bookingForm.value.fin,
      };

      this.submitted.emit(booking);
    } else {
      this.validationService.markFormControlsAsTouched(this.bookingForm);
    }
  }

  private initBookingForm(): void {
    this.bookingForm = new FormGroup({
      range: new FormGroup({
        start: new FormControl(new Date(), Validators.required),
        end: new FormControl(new Date(), Validators.required),
      }),
      employeeNumber: new FormControl(null, Validators.required),
      fin: new FormControl(null, Validators.required),
    });
  }

  private setBookingFormValue(booking: Booking): void {
    const value: formValue = {
      range: {
        start: booking?.start,
        end: booking?.end,
      },
      employeeNumber: booking?.employeeNumber,
      fin: booking?.fin,
    };

    this.bookingForm.setValue(value);
  }
}

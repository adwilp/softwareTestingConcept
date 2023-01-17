import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FlatVehicle } from 'src/app/vehicles/models/flat-vehicle.model';
import { VehicleFacade } from 'src/app/vehicles/_store/vehicle.facade';
import { ValidationService } from 'src/app/shared/validation.service';
import { Booking } from '../models/booking.model';

@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.scss'],
})
export class BookingFormComponent implements OnInit {
  @Output() submitted: EventEmitter<Booking>;

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
}

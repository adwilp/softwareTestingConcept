import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ValidationService } from 'src/app/shared/validation.service';
import { FlatVehicle } from 'src/app/vehicles/models/flat-vehicle.model';
import { VehicleFacade } from 'src/app/vehicles/_store/vehicle.facade';
import { Booking } from '../models/booking.model';
import { BookingFacade } from '../_store/booking.facade';

@Component({
  selector: 'app-add-booking',
  templateUrl: './add-booking.component.html',
  styleUrls: ['./add-booking.component.scss'],
})
export class AddBookingComponent implements OnInit {
  bookingForm: FormGroup;

  get vehicles$(): Observable<FlatVehicle[]> {
    return this.vehicleFacade.vehicles$;
  }

  constructor(
    private bookingFacade: BookingFacade,
    private vehicleFacade: VehicleFacade,
    private validationService: ValidationService
  ) {}

  ngOnInit(): void {
    this.initBookingForm();
    this.vehicleFacade.getVehicles();
  }

  bookingSubmited(): void {
    if (this.bookingForm.valid) {
      const booking: Booking = {
        start: this.bookingForm.value.range.start,
        end: this.bookingForm.value.range.end,
        employeeNumber: this.bookingForm.value.employeeNumber,
        fin: this.bookingForm.value.fin,
      };

      this.bookingFacade.addBooking(booking);
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

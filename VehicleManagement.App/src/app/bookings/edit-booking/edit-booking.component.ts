import { Component } from '@angular/core';
import { Booking } from '../models/booking.model';
import { UpdateableBooking } from '../models/updateable-booking.model';
import { BookingFacade } from '../_store/booking.facade';

@Component({
  selector: 'app-edit-booking',
  templateUrl: './edit-booking.component.html',
  styleUrls: ['./edit-booking.component.scss'],
})
export class EditBookingComponent {
  private _booking: Booking;

  public get booking(): Booking {
    return this._booking;
  }

  constructor(private bookingFacade: BookingFacade) {}

  bookingSubmited(booking: Booking): void {
    const update: UpdateableBooking = {
      ...booking,
      id: 1,
    };

    this.bookingFacade.editBooking(update);
  }
}

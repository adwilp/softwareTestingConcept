import { Component } from '@angular/core';
import { Booking } from '../models/booking.model';
import { BookingFacade } from '../_store/booking.facade';

@Component({
  selector: 'app-add-booking',
  templateUrl: './add-booking.component.html',
  styleUrls: ['./add-booking.component.scss'],
})
export class AddBookingComponent {
  constructor(private bookingFacade: BookingFacade) {}

  bookingSubmited(booking: Booking): void {
    this.bookingFacade.addBooking(booking);
  }
}

import { Component, OnInit } from '@angular/core';
import { FlatBooking } from '../models/flat-booking.model';
import { BookingFacade } from '../_store/booking.facade';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-booking-overview',
  templateUrl: './booking-overview.component.html',
  styleUrls: ['./booking-overview.component.scss'],
})
export class BookingOverviewComponent implements OnInit {
  get displayedColumns(): string[] {
    return ['fIN', 'licensePlate', 'color', 'mileage', 'manufacturer'];
  }

  get vehicles$(): Observable<FlatBooking[]> {
    return this.bookingFacade.bookings$;
  }

  constructor(private bookingFacade: BookingFacade) {}

  ngOnInit(): void {
    this.bookingFacade.getBookings();
  }
}

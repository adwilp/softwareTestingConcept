import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Booking } from '../models/booking.model';
import { FlatBooking } from '../models/flat-booking.model';
import { UpdateableBooking } from '../models/updateable-booking.model';
import * as BookingActions from './booking.actions';
import { BookingQuery } from './booking.selectors';

@Injectable({ providedIn: 'root' })
export class BookingFacade {
  readonly bookings$: Observable<FlatBooking[]> = this.store.select(
    BookingQuery.selectBookings
  );

  readonly bookingsLoading$: Observable<boolean> = this.store.select(
    BookingQuery.selectBookingsLoading
  );

  constructor(private store: Store) {}

  getBookings(): void {
    this.store.dispatch(BookingActions.getBookings());
  }

  addBooking(booking: Booking): void {
    this.store.dispatch(BookingActions.addBooking({ booking: booking }));
  }

  editBooking(booking: UpdateableBooking): void {
    this.store.dispatch(BookingActions.editBooking({ booking: booking }));
  }
}

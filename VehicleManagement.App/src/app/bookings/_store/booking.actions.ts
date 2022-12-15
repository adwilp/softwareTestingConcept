/* eslint-disable @typescript-eslint/typedef */
import { createAction, props } from '@ngrx/store';
import { FlatBooking } from '../models/flat-booking.model';

// VEHICLES
export const getBookings = createAction('[Bookings] Get bookings');

export type getBookingsSuccess = { bookings: FlatBooking[] };
export const getBookingsSuccess = createAction(
  '[Bookings] Get bookings - Success',
  props<getBookingsSuccess>()
);

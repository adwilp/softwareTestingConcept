/* eslint-disable @typescript-eslint/typedef */
import { createAction, props } from '@ngrx/store';
import { Booking } from '../models/booking.model';
import { FlatBooking } from '../models/flat-booking.model';
import { UpdateableBooking } from '../models/updateable-booking.model';

// BOOKINGS
export const getBookings = createAction('[Bookings] Get bookings');

export type getBookingsSuccess = { bookings: FlatBooking[] };
export const getBookingsSuccess = createAction(
  '[Bookings] Get bookings - Success',
  props<getBookingsSuccess>()
);

export type addBooking = { booking: Booking };
export const addBooking = createAction(
  '[Bookings] Add booking',
  props<addBooking>()
);

export type addBookingSuccess = { booking: FlatBooking };
export const addBookingSuccess = createAction(
  '[Bookings] Add booking - Success',
  props<addBookingSuccess>()
);

export type getBooking = { id: number };
export const getBooking = createAction(
  '[Bookings] Get booking',
  props<getBooking>()
);

export type getBookingSuccess = { booking: UpdateableBooking };
export const getBookingSuccess = createAction(
  '[Bookings] Get booking - Success',
  props<getBookingSuccess>()
);

export type editBooking = { booking: UpdateableBooking };
export const editBooking = createAction(
  '[Bookings] Edit booking',
  props<editBooking>()
);

export type editBookingSuccess = { booking: FlatBooking };
export const editBookingSuccess = createAction(
  '[Bookings] Edit booking - Success',
  props<editBookingSuccess>()
);

/* eslint-disable @typescript-eslint/typedef */
import { createFeatureSelector, createSelector } from '@ngrx/store';
import { bookingFeatureKey, BookingState } from './booking.reducers';

const selectBookingState =
  createFeatureSelector<BookingState>(bookingFeatureKey);

// VEHICLES
const selectBookingsLoading = createSelector(
  selectBookingState,
  (state: BookingState) => state.bookingsLoading
);

const selectBookings = createSelector(
  selectBookingState,
  (state: BookingState) => state.bookings
);

const selectnewBookingProcessing = createSelector(
  selectBookingState,
  (state: BookingState) => state.newBookingProcessing
);

export const BookingQuery = {
  selectBookings,
  selectBookingsLoading,
  selectnewBookingProcessing,
};

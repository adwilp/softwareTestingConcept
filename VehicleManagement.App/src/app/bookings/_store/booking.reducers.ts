import { Action, ActionReducer, on } from '@ngrx/store';
import { createImmerReducer } from 'ngrx-immer/store';
import { FlatBooking } from '../models/flat-booking.model';
import * as BookingActions from './booking.actions';

/* eslint-disable @typescript-eslint/typedef */
export const bookingFeatureKey = 'booking';

export type BookingState = {
  bookings: FlatBooking[];
  bookingsLoading: boolean;
  newBookingProcessing: boolean;
};

export function createVehicleInitialState(): BookingState {
  return {
    bookings: [],
    bookingsLoading: false,
    newBookingProcessing: false,
  };
}

const reducer: ActionReducer<BookingState> = createImmerReducer(
  createVehicleInitialState(),

  on(BookingActions.getBookings, (state: BookingState) => {
    state.bookingsLoading = true;
    return state;
  }),

  on(
    BookingActions.getBookingsSuccess,
    (state: BookingState, { bookings }: BookingActions.getBookingsSuccess) => {
      state.bookings = bookings;
      state.bookingsLoading = false;
      return state;
    }
  ),

  on(BookingActions.addBooking, (state: BookingState) => {
    state.newBookingProcessing = true;
    return state;
  })
);

export function bookingReducer(
  state: BookingState | undefined,
  action: Action
): BookingState {
  return reducer(state, action);
}

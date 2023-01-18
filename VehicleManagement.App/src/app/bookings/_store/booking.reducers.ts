import { Action, ActionReducer, on } from '@ngrx/store';
import { createImmerReducer } from 'ngrx-immer/store';
import { FlatBooking } from '../models/flat-booking.model';
import { UpdateableBooking } from '../models/updateable-booking.model';
import * as BookingActions from './booking.actions';

/* eslint-disable @typescript-eslint/typedef */
export const bookingFeatureKey = 'booking';

export type BookingState = {
  bookings: FlatBooking[];
  bookingsLoading: boolean;
  newBookingProcessing: boolean;
  editBookingProcessing: boolean;
  bookingLoading: boolean;
  selectedBooking: UpdateableBooking;
};

export function createVehicleInitialState(): BookingState {
  return {
    bookings: [],
    bookingsLoading: false,
    newBookingProcessing: false,
    editBookingProcessing: false,
    bookingLoading: false,
    selectedBooking: null,
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
  }),

  on(
    BookingActions.addBookingSuccess,
    (state: BookingState, { booking }: BookingActions.addBookingSuccess) => {
      state.newBookingProcessing = false;

      const bookings: FlatBooking[] = state.bookings;
      bookings.push(booking);

      state.bookings = bookings;
      return state;
    }
  ),

  on(BookingActions.editBooking, (state: BookingState) => {
    state.editBookingProcessing = true;

    return state;
  }),

  on(BookingActions.editBookingSuccess, (state: BookingState) => {
    state.editBookingProcessing = false;

    return state;
  }),

  on(BookingActions.getBooking, (state: BookingState) => {
    state.bookingLoading = true;
    return state;
  }),

  on(
    BookingActions.getBookingSuccess,
    (state: BookingState, { booking }: BookingActions.getBookingSuccess) => {
      state.bookingLoading = false;
      state.selectedBooking = booking;
      return state;
    }
  )
);

export function bookingReducer(
  state: BookingState | undefined,
  action: Action
): BookingState {
  return reducer(state, action);
}

import { Action } from '@ngrx/store';
import {
  createBookingInitialState,
  bookingReducer,
  BookingState,
} from './booking.reducers';
import * as BookingsActions from './booking.actions';
import { booking, flatBookings, updateableBooking } from './booking.test-data';
import { TypedAction } from '@ngrx/store/src/models';

describe('BookingReducer', () => {
  let initState: BookingState;

  beforeEach(() => {
    initState = createBookingInitialState();
  });

  describe('undefined action', () => {
    it('returns the initial state for undefined action', () => {
      // ARRANGE
      const action: Action = {} as Action;

      // ACT
      const state: BookingState = bookingReducer(undefined, action);

      // ASSERT
      expect(state).toEqual(initState);
    });
  });

  describe('getBookings action', () => {
    it('returns the getBookings state', () => {
      // ARRANGE
      const expectedState: BookingState = structuredClone(initState);
      expectedState.bookingsLoading = true;

      const action: TypedAction<'[Bookings] Get bookings'> =
        BookingsActions.getBookings();

      // ACT
      const state: BookingState = bookingReducer(initState, action);

      // ASSERT
      expect(state).toEqual(expectedState);
    });
  });

  describe('getBookingsSuccess action', () => {
    it('returns the flat bookings', () => {
      // ARRANGE
      const expectedState: BookingState = structuredClone(initState);
      expectedState.bookings = flatBookings;

      const action: BookingsActions.getBookingsSuccess &
        TypedAction<'[Bookings] Get bookings - Success'> =
        BookingsActions.getBookingsSuccess({
          bookings: flatBookings,
        });

      // ACT
      const state: BookingState = bookingReducer(initState, action);

      // ASSERT
      expect(state).toEqual(expectedState);
    });
  });

  describe('getBooking action', () => {
    it('returns the getBooking state', () => {
      // ARRANGE
      const expectedState: BookingState = structuredClone(initState);
      expectedState.bookingLoading = true;

      const action: TypedAction<'[Bookings] Get booking'> =
        BookingsActions.getBooking({ id: 12 });

      // ACT
      const state: BookingState = bookingReducer(initState, action);

      // ASSERT
      expect(state).toEqual(expectedState);
    });
  });

  describe('getBookingSuccess action', () => {
    it('returns the selected booking', () => {
      // ARRANGE
      const expectedState: BookingState = structuredClone(initState);
      expectedState.bookingLoading = false;
      expectedState.selectedBooking = updateableBooking;

      const action: BookingsActions.getBookingSuccess &
        TypedAction<'[Bookings] Get booking - Success'> =
        BookingsActions.getBookingSuccess({
          booking: updateableBooking,
        });

      // ACT
      const state: BookingState = bookingReducer(initState, action);

      // ASSERT
      expect(state).toEqual(expectedState);
    });
  });

  describe('addBooking action', () => {
    it('returns the addBooking state', () => {
      // ARRANGE
      const expectedState: BookingState = structuredClone(initState);
      expectedState.newBookingProcessing = true;

      const action: TypedAction<'[Bookings] Add booking'> =
        BookingsActions.addBooking({ booking: booking });

      // ACT
      const state: BookingState = bookingReducer(initState, action);

      // ASSERT
      expect(state).toEqual(expectedState);
    });
  });

  describe('addBookingSuccess action', () => {
    it('returns the new flat booking', () => {
      // ARRANGE
      const expectedState: BookingState = structuredClone(initState);
      expectedState.bookings = flatBookings;

      const action: BookingsActions.addBookingSuccess &
        TypedAction<'[Bookings] Add booking - Success'> =
        BookingsActions.addBookingSuccess({
          booking: flatBookings[0],
        });

      // ACT
      const state: BookingState = bookingReducer(initState, action);

      // ASSERT
      expect(state).toEqual(expectedState);
    });
  });

  describe('editBooking action', () => {
    it('returns the editBooking state', () => {
      // ARRANGE
      const expectedState: BookingState = structuredClone(initState);
      expectedState.editBookingProcessing = true;

      const action: TypedAction<'[Bookings] Edit booking'> =
        BookingsActions.editBooking({ booking: updateableBooking });

      // ACT
      const state: BookingState = bookingReducer(initState, action);

      // ASSERT
      expect(state).toEqual(expectedState);
    });
  });

  describe('editBookingSuccess action', () => {
    it('returns the new flat booking', () => {
      // ARRANGE
      const expectedState: BookingState = structuredClone(initState);
      expectedState.editBookingProcessing = false;

      const action: BookingsActions.editBookingSuccess &
        TypedAction<'[Bookings] Edit booking - Success'> =
        BookingsActions.editBookingSuccess({
          booking: flatBookings[0],
        });

      // ACT
      const state: BookingState = bookingReducer(initState, action);

      // ASSERT
      expect(state).toEqual(expectedState);
    });
  });
});

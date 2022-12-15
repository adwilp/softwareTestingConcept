import { Action } from '@ngrx/store';
import {
  createVehicleInitialState,
  bookingReducer,
  BookingState,
} from './booking.reducers';
import * as BookingsActions from './booking.actions';
import { flatBookings } from './booking.test-data';
import { TypedAction } from '@ngrx/store/src/models';

describe('VehicleReducer', () => {
  let initState: BookingState;

  beforeEach(() => {
    initState = createVehicleInitialState();
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
});

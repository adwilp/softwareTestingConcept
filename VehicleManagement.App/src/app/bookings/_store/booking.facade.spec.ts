import { MockStore, provideMockStore } from '@ngrx/store/testing';
import { TestBed } from '@angular/core/testing';
import { Store } from '@ngrx/store';
import { booking, flatBookings, updateableBooking } from './booking.test-data';
import { BookingFacade } from './booking.facade';
import { first } from 'rxjs';
import { FlatBooking } from '../models/flat-booking.model';
import * as BookingActions from './booking.actions';
import { TypedAction } from '@ngrx/store/src/models';
import { AppState } from 'src/app/_store/app.reducer';
import { testState } from 'src/app/_store/state.test-data';

describe('BookingFacade', () => {
  let facade: BookingFacade;
  let store: MockStore<AppState>;
  const initialState: unknown = {};

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BookingFacade, provideMockStore({ initialState })],
    });

    facade = TestBed.inject(BookingFacade);
    store = TestBed.inject<Store>(Store) as MockStore<AppState>;
  });

  it('returns all bookings', (done: DoneFn) => {
    // ARRANGE
    let currentBookings: FlatBooking[] | undefined;

    const state: AppState = testState;
    state.booking.bookings = flatBookings;
    store.setState(state);

    // ACT
    facade.bookings$.pipe(first()).subscribe((vehicles: FlatBooking[]) => {
      currentBookings = vehicles;
      done();
    });

    // ASSERT
    if (!currentBookings) {
      fail('currentBookings must not be undefined!');
    }

    expect(currentBookings).toEqual(flatBookings);
  });

  it('returns bookings loading', (done: DoneFn) => {
    // ARRANGE
    let currentBookingsLoading: boolean | undefined;

    const state: AppState = testState;
    state.booking.bookingsLoading = true;
    store.setState(state);

    // ACT
    facade.bookingsLoading$
      .pipe(first())
      .subscribe((vehiclesLoading: boolean) => {
        currentBookingsLoading = vehiclesLoading;
        done();
      });

    // ASSERT
    if (!currentBookingsLoading) {
      fail('currentBookingsLoading must not be undefined!');
    }

    expect(currentBookingsLoading).toEqual(true);
  });

  it('should dispatch getBookings action', () => {
    // ARRANGE
    const expectedAction: TypedAction<'[Bookings] Get bookings'> =
      BookingActions.getBookings();
    spyOn(store, 'dispatch');

    // ACT
    facade.getBookings();

    // ASSERT
    expect(store.dispatch).toHaveBeenCalledWith(expectedAction);
  });

  it('should dispatch addBooking action', () => {
    // ARRANGE
    const expectedAction: TypedAction<'[Bookings] Add booking'> =
      BookingActions.addBooking({ booking: booking });
    spyOn(store, 'dispatch');

    // ACT
    facade.addBooking(booking);

    // ASSERT
    expect(store.dispatch).toHaveBeenCalledWith(expectedAction);
  });

  it('should dispatch editBooking action', () => {
    // ARRANGE
    const expectedAction: TypedAction<'[Bookings] Edit booking'> =
      BookingActions.editBooking({ booking: updateableBooking });
    spyOn(store, 'dispatch');

    // ACT
    facade.editBooking(updateableBooking);

    // ASSERT
    expect(store.dispatch).toHaveBeenCalledWith(expectedAction);
  });

  it('should dispatch getBooking action', () => {
    // ARRANGE
    const expectedAction: TypedAction<'[Bookings] Get booking'> =
      BookingActions.getBooking({ id: updateableBooking.id });
    spyOn(store, 'dispatch');

    // ACT
    facade.getBooking(updateableBooking.id);

    // ASSERT
    expect(store.dispatch).toHaveBeenCalledWith(expectedAction);
  });

  it('should dispatch deleteBooking action', () => {
    // ARRANGE
    const expectedAction: TypedAction<'[Bookings] Delete booking'> =
      BookingActions.deleteBooking({ id: updateableBooking.id });
    spyOn(store, 'dispatch');

    // ACT
    facade.deleteBooking(updateableBooking.id);

    // ASSERT
    expect(store.dispatch).toHaveBeenCalledWith(expectedAction);
  });
});

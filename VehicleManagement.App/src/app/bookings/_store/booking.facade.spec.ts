import { MockStore, provideMockStore } from '@ngrx/store/testing';
import { TestBed } from '@angular/core/testing';
import { Store } from '@ngrx/store';
import { flatBookings } from './booking.test-data';
import { BookingFacade } from './booking.facade';
import { first } from 'rxjs';
import { FlatBooking } from '../models/flat-booking.model';
import * as BookingActions from './booking.actions';
import { TypedAction } from '@ngrx/store/src/models';
import { AppState } from 'src/app/_store/app.reducer';
import { flatVehicles } from 'src/app/vehicles/_store/vehicle.test-data';

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
    store.setState({
      vehicle: {
        vehicles: flatVehicles,
        vehiclesLoading: false,
      },
      booking: {
        bookings: flatBookings,
        bookingsLoading: false,
      },
    });

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
    store.setState({
      vehicle: {
        vehicles: flatVehicles,
        vehiclesLoading: false,
      },
      booking: {
        bookings: flatBookings,
        bookingsLoading: true,
      },
    });

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
});

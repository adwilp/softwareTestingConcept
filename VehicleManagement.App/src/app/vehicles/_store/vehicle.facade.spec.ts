import { AppState } from 'src/app/_store/app.reducer';
import { MockStore, provideMockStore } from '@ngrx/store/testing';
import { TestBed } from '@angular/core/testing';
import { Store } from '@ngrx/store';
import { flatVehicles } from './vehicle.test-data';
import { VehicleFacade } from './vehicle.facade';
import { first } from 'rxjs';
import { FlatVehicle } from '../models/flat-vehicle.model';
import * as VehicleActions from './vehicle.actions';
import { TypedAction } from '@ngrx/store/src/models';
import { flatBookings } from 'src/app/bookings/_store/booking.test-data';

describe('VehicleFacade', () => {
  let facade: VehicleFacade;
  let store: MockStore<AppState>;
  const initialState: unknown = {};

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VehicleFacade, provideMockStore({ initialState })],
    });

    facade = TestBed.inject(VehicleFacade);
    store = TestBed.inject<Store>(Store) as MockStore<AppState>;
  });

  it('returns all vehicles', (done: DoneFn) => {
    // ARRANGE
    let currentVehicles: FlatVehicle[] | undefined;
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
    facade.vehicles$.pipe(first()).subscribe((vehicles: FlatVehicle[]) => {
      currentVehicles = vehicles;
      done();
    });

    // ASSERT
    if (!currentVehicles) {
      fail('currentVehicles must not be undefined!');
    }

    expect(currentVehicles).toEqual(flatVehicles);
  });

  it('returns vehicles loading', (done: DoneFn) => {
    // ARRANGE
    let currentVehiclesLoading: boolean | undefined;
    store.setState({
      vehicle: {
        vehicles: flatVehicles,
        vehiclesLoading: true,
      },
      booking: {
        bookings: flatBookings,
        bookingsLoading: false,
      },
    });

    // ACT
    facade.vehiclesLoading$
      .pipe(first())
      .subscribe((vehiclesLoading: boolean) => {
        currentVehiclesLoading = vehiclesLoading;
        done();
      });

    // ASSERT
    if (!currentVehiclesLoading) {
      fail('currentVehiclesLoading must not be undefined!');
    }

    expect(currentVehiclesLoading).toEqual(true);
  });

  it('should dispatch getVehicles action', () => {
    // ARRANGE
    const expectedAction: TypedAction<'[Vehicles] Get vehicles'> =
      VehicleActions.getVehicles();
    spyOn(store, 'dispatch');

    // ACT
    facade.getVehicles();

    // ASSERT
    expect(store.dispatch).toHaveBeenCalledWith(expectedAction);
  });
});

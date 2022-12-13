import { TestBed } from '@angular/core/testing';
import { Observable, of, throwError } from 'rxjs';
import { provideMockActions } from '@ngrx/effects/testing';
import { VehicleEffects } from './vehicle.effects';
import { vehicleReducer } from './vehicle.reducers';
import { VehicleService } from '../vehicle.service';
import { MockProviders } from 'ng-mocks';
import * as VehicleActions from './vehicle.actions';
import Spy = jasmine.Spy;
import { HttpErrorResponse } from '@angular/common/http';
import { flatVehicles } from './vehicle.test-data';
import { TypedAction } from '@ngrx/store/src/models';

describe('VehicleEffects', () => {
  type getVehiclesSuccessAction = VehicleActions.getVehiclesSuccess &
    TypedAction<'[Vehicles] Get vehicles - Success'>;

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  let actionsMock$: Observable<any>;
  let effects: VehicleEffects;
  let vehicleService: VehicleService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideMockActions(() => actionsMock$),
        VehicleEffects,
        MockProviders(vehicleReducer, VehicleService),
      ],
    });

    vehicleService = TestBed.inject(VehicleService);
    effects = TestBed.inject(VehicleEffects);
  });

  describe('getVehicles$', () => {
    let vehicleSpy: Spy;

    beforeEach(() => {
      actionsMock$ = of(VehicleActions.getVehicles);
      vehicleSpy = spyOn(vehicleService, 'getVehicles');
    });

    it('calls getVehicles once', () => {
      // ARRANGE
      vehicleSpy.and.returnValue(of(flatVehicles));

      // ACT
      effects.getVehicles$.subscribe();

      // ASSERT
      expect(vehicleSpy).toHaveBeenCalledOnceWith();
    });

    it('returns a stream with FlatVehicle', () => {
      // ARRANGE
      let currentOutcome: getVehiclesSuccessAction | undefined;
      const outcome: getVehiclesSuccessAction =
        VehicleActions.getVehiclesSuccess({
          vehicles: flatVehicles,
        });
      vehicleSpy.and.returnValue(of(flatVehicles));

      // ACT
      effects.getVehicles$.subscribe((action: getVehiclesSuccessAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        vehicles: outcome.vehicles,
      });
    });

    it('returns a stream with null on http error', () => {
      // ARRANGE
      let currentOutcome: getVehiclesSuccessAction | undefined;
      const error: HttpErrorResponse = new HttpErrorResponse({});
      const outcome: getVehiclesSuccessAction =
        VehicleActions.getVehiclesSuccess({ vehicles: null });
      vehicleSpy.and.callFake(() => {
        return throwError(() => error);
      });

      // ACT
      effects.getVehicles$.subscribe((action: getVehiclesSuccessAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        vehicles: outcome.vehicles,
      });
    });
  });
});

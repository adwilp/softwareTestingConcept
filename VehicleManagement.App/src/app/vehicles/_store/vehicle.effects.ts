import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { VehicleService } from '../vehicle.service';
import { catchError, map, of, switchMap } from 'rxjs';
import * as VehicleActions from './vehicle.actions';
import * as AppActions from '../../_store/app-actions';
import { FlatVehicle } from '../models/flat-vehicle.model';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class VehicleEffects {
  // eslint-disable-next-line @typescript-eslint/typedef
  getVehicles$ = createEffect(() => {
    return this.action$.pipe(
      ofType(VehicleActions.getVehicles),
      switchMap(() => {
        return this.vehicleService.getVehicles().pipe(
          map((vehicles: FlatVehicle[]) => {
            return VehicleActions.getVehiclesSuccess({
              vehicles: vehicles,
            });
          }),
          catchError((error: HttpErrorResponse) => {
            return of(
              AppActions.appError({
                message: 'Error loading all vehicles!',
                error: error,
              })
            );
          })
        );
      })
    );
  });

  constructor(
    private action$: Actions,
    private vehicleService: VehicleService
  ) {}
}

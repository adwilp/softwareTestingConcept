/* eslint-disable @typescript-eslint/typedef */
import { createAction, props } from '@ngrx/store';
import { FlatVehicle } from '../models/flat-vehicle.model';

// VEHICLES
export const getVehicles = createAction('[Vehicles] Get vehicles');

export type getVehiclesSuccess = { vehicles: FlatVehicle[] };
export const getVehiclesSuccess = createAction(
  '[Vehicles] Get vehicles - Success',
  props<getVehiclesSuccess>()
);

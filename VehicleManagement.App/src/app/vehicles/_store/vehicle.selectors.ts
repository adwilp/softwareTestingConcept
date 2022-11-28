/* eslint-disable @typescript-eslint/typedef */
import { createFeatureSelector, createSelector } from '@ngrx/store';
import { VehicleState, vehicleFeatureKey } from './vehicle.reducers';

const selectVehicleState =
  createFeatureSelector<VehicleState>(vehicleFeatureKey);

// VEHICLES
const selectVehiclesLoading = createSelector(
  selectVehicleState,
  (state: VehicleState) => state.vehiclesLoading
);

const selectVehicles = createSelector(
  selectVehicleState,
  (state: VehicleState) => state.vehicles
);

export const VehicleQuery = {
  selectVehicles,
  selectVehiclesLoading,
};

import { Action, ActionReducer, on } from '@ngrx/store';
import { createImmerReducer } from 'ngrx-immer/store';
import { FlatVehicle } from '../models/flat-vehicle.model';
import * as VehiclesActions from './vehicle.actions';

/* eslint-disable @typescript-eslint/typedef */
export const vehicleFeatureKey = 'vehicle';

export type VehicleState = {
  vehicles: FlatVehicle[];
  vehiclesLoading: boolean;
};

function createVehicleInitialState(): VehicleState {
  return {
    vehicles: [],
    vehiclesLoading: false,
  };
}

const reducer: ActionReducer<VehicleState> = createImmerReducer(
  createVehicleInitialState(),

  on(VehiclesActions.getVehicles, (state: VehicleState) => {
    state.vehiclesLoading = true;
    return state;
  }),

  on(
    VehiclesActions.getVehiclesSuccess,
    (state: VehicleState, { vehicles }: VehiclesActions.getVehiclesSuccess) => {
      state.vehicles = vehicles;
      state.vehiclesLoading = false;
      return state;
    }
  )
);

export function vehicleReducer(
  state: VehicleState | undefined,
  action: Action
): VehicleState {
  return reducer(state, action);
}

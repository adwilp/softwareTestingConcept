import { ActionReducerMap } from '@ngrx/store';
import {
  vehicleFeatureKey,
  vehicleReducer,
  VehicleState,
} from '../vehicles/_store/vehicle.reducers';

export type AppState = {
  [vehicleFeatureKey]: VehicleState;
};

export const AppReducer: ActionReducerMap<AppState> = {
  [vehicleFeatureKey]: vehicleReducer,
};

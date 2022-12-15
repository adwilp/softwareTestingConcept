import { ActionReducerMap } from '@ngrx/store';
import {
  bookingFeatureKey,
  bookingReducer,
  BookingState,
} from '../bookings/_store/booking.reducers';
import {
  vehicleFeatureKey,
  vehicleReducer,
  VehicleState,
} from '../vehicles/_store/vehicle.reducers';

export type AppState = {
  [vehicleFeatureKey]: VehicleState;
  [bookingFeatureKey]: BookingState;
};

export const AppReducer: ActionReducerMap<AppState> = {
  [vehicleFeatureKey]: vehicleReducer,
  [bookingFeatureKey]: bookingReducer,
};

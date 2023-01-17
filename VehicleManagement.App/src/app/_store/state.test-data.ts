import { AppState } from './app.reducer';

export const testState: AppState = {
  vehicle: {
    vehicles: [],
    vehiclesLoading: false,
  },
  booking: {
    bookings: [],
    bookingsLoading: true,
    newBookingProcessing: false,
    editBookingProcessing: false,
  },
};

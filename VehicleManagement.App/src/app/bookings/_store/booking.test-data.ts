import { Booking } from '../models/booking.model';
import { FlatBooking } from '../models/flat-booking.model';
import { UpdateableBooking } from '../models/updateable-booking.model';

export const flatBookings: FlatBooking[] = [
  {
    id: 1,
    start: new Date(''),
    end: new Date(''),
    employeeNumber: '',
    fin: '',
    licensePlate: '',
  },
];

export const booking: Booking = {
  start: new Date(''),
  end: new Date(''),
  employeeNumber: '',
  fin: '',
};

export const updateableBooking: UpdateableBooking = {
  id: 12,
  start: new Date(''),
  end: new Date(''),
  employeeNumber: '',
  fin: '',
};

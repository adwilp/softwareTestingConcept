import {
  createComponentFactory,
  mockProvider,
  Spectator,
  SpectatorFactory,
  SpyObject,
} from '@ngneat/spectator';
import { TranslatePipe } from '@ngx-translate/core';
import { MockPipe } from 'ng-mocks';
import { ValidationService } from 'src/app/shared/validation.service';
import { VehicleFacade } from 'src/app/vehicles/_store/vehicle.facade';
import { Booking } from '../models/booking.model';
import { BookingFacade } from '../_store/booking.facade';

import { AddBookingComponent } from './add-booking.component';

describe('AddBookingComponent', () => {
  let spectator: Spectator<AddBookingComponent>;

  const createAddBookingComponent: SpectatorFactory<AddBookingComponent> =
    createComponentFactory({
      component: AddBookingComponent,
      shallow: true,
      declarations: [MockPipe(TranslatePipe)],
      providers: [
        mockProvider(VehicleFacade),
        mockProvider(BookingFacade),
        mockProvider(ValidationService),
      ],
    });

  beforeEach(async () => {
    spectator = createAddBookingComponent();
  });

  it('creates component', () => {
    // ASSERT
    expect(spectator.component).toBeTruthy();
  });

  it('loads all vehicles on init', () => {
    // ARRANGE
    const facade: SpyObject<VehicleFacade> = spectator.inject(VehicleFacade);

    // ASSERT
    expect(facade.getVehicles).toHaveBeenCalled();
  });

  it('initializes bookingForm on init', () => {
    // ASSERT
    expect(spectator.component).toBeDefined();
  });

  it('finds correct error for true', () => {
    // ARRANGE
    spectator.component.bookingForm.get(['range', 'start']).markAsTouched();

    spectator.component.bookingForm
      .get(['range', 'start'])
      .setErrors({ required: true });

    // ACT
    const result: boolean = spectator.component.isError(
      'range.start',
      'required'
    );

    // ASSERT
    expect(result).toBe(true);
  });

  it('finds correct error for false', () => {
    // ARRANGE
    spectator.component.bookingForm.get(['range', 'end']).markAsTouched();

    spectator.component.bookingForm
      .get(['range', 'end'])
      .setErrors({ required: true });

    // ACT
    const result: boolean = spectator.component.isError(
      'range.start',
      'required'
    );

    // ASSERT
    expect(result).toBe(false);
  });

  it('it touches all controls of bookingForm on submit with error', () => {
    // ARRANGE
    const validationService: SpyObject<ValidationService> =
      spectator.inject(ValidationService);

    // ACT
    spectator.component.bookingSubmited();

    // ASSERT
    expect(
      validationService.markFormControlsAsTouched
    ).toHaveBeenCalledOnceWith(spectator.component.bookingForm);
  });

  it('it adds booking on submit', () => {
    // ARRANGE
    const bookingFacade: SpyObject<BookingFacade> =
      spectator.inject(BookingFacade);
    spectator.component.bookingForm.controls.employeeNumber.setValue('fin');
    spectator.component.bookingForm.controls.fin.setValue('fin');

    const booking: Booking = {
      start: spectator.component.bookingForm.get(['range', 'start']).value,
      end: spectator.component.bookingForm.get(['range', 'end']).value,
      employeeNumber: spectator.component.bookingForm.get(['employeeNumber'])
        .value,
      fin: spectator.component.bookingForm.get(['fin']).value,
    };

    // ACT
    spectator.component.bookingSubmited();

    // ASSERT
    expect(bookingFacade.addBooking).toHaveBeenCalledOnceWith(booking);
  });
});

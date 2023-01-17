import { DebugElement } from '@angular/core';
import { By } from '@angular/platform-browser';
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
import { BookingFacade } from '../_store/booking.facade';
import { booking } from '../_store/booking.test-data';
import { AddBookingComponent } from './add-booking.component';
import Spy = jasmine.Spy;

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

  it('it reacts to submitted event', () => {
    // ARRANGE
    const bookingSubmited: Spy = spyOn(spectator.component, 'bookingSubmited');
    const bookingForm: DebugElement = spectator.fixture.debugElement.query(
      By.css('app-booking-form')
    );

    // ACT
    bookingForm.triggerEventHandler('submitted', booking);

    // ASSERT
    expect(bookingSubmited).toHaveBeenCalledOnceWith(booking);
  });

  it('it adds booking on bookingSubmitted', () => {
    // ARRANGE
    const facade: SpyObject<BookingFacade> = spectator.inject(BookingFacade);

    // ACT
    spectator.component.bookingSubmited(booking);

    // ASSERT
    expect(facade.addBooking).toHaveBeenCalledOnceWith(booking);
  });
});

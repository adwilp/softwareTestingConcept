import { ActivatedRoute } from '@angular/router';
import {
  createComponentFactory,
  mockProvider,
  Spectator,
  SpectatorFactory,
  SpyObject,
} from '@ngneat/spectator';

import { TranslatePipe } from '@ngx-translate/core';
import { MockPipe } from 'ng-mocks';
import { of } from 'rxjs';
import { BookingFacade } from '../_store/booking.facade';
import { EditBookingComponent } from './edit-booking.component';
import { updateableBooking } from '../_store/booking.test-data';

describe('EditBookingComponent', () => {
  let spectator: Spectator<EditBookingComponent>;

  const createEditBookingComponent: SpectatorFactory<EditBookingComponent> =
    createComponentFactory({
      component: EditBookingComponent,
      shallow: true,
      declarations: [MockPipe(TranslatePipe)],
      providers: [
        mockProvider(BookingFacade, {
          booking$: of(updateableBooking),
        }),
        mockProvider(ActivatedRoute, {
          params: of({
            id: 2,
          }),
        }),
      ],
    });

  beforeEach(async () => {
    spectator = createEditBookingComponent();
  });

  it('should create', () => {
    // ASSERT
    expect(spectator.component).toBeTruthy();
  });

  it('loads selected booking on init', () => {
    // ARRANGE
    const facade: SpyObject<BookingFacade> = spectator.inject(BookingFacade);

    // ASSERT
    expect(facade.getBooking).toHaveBeenCalledOnceWith(2);
  });

  it('submits edit booking in store on submit', () => {
    const facade: SpyObject<BookingFacade> = spectator.inject(BookingFacade);

    spectator.component.bookingSubmited(updateableBooking);

    // ASSERT
    expect(facade.editBooking).toHaveBeenCalledOnceWith(updateableBooking);
  });
});

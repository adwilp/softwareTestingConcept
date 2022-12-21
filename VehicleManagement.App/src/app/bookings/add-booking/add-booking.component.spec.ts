import { Router } from '@angular/router';
import {
  createComponentFactory,
  Spectator,
  SpectatorFactory,
} from '@ngneat/spectator';
import { TranslatePipe } from '@ngx-translate/core';
import { MockPipe, MockProviders } from 'ng-mocks';
import { BookingFacade } from '../_store/booking.facade';

import { AddBookingComponent } from './add-booking.component';

describe('AddBookingComponent', () => {
  let spectator: Spectator<AddBookingComponent>;

  const createAddBookingComponent: SpectatorFactory<AddBookingComponent> =
    createComponentFactory({
      component: AddBookingComponent,
      shallow: true,
      declarations: [MockPipe(TranslatePipe)],
      providers: [MockProviders(BookingFacade, Router)],
    });

  beforeEach(async () => {
    spectator = createAddBookingComponent();
  });

  it('creates component', () => {
    expect(spectator.component).toBeTruthy();
  });
});

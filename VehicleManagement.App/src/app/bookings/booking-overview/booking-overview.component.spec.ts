import {
  createComponentFactory,
  mockProvider,
  Spectator,
  SpectatorFactory,
  SpyObject,
} from '@ngneat/spectator';
import { TranslatePipe } from '@ngx-translate/core';
import { MockPipe } from 'ng-mocks';
import { BookingFacade } from '../_store/booking.facade';
import { BookingOverviewComponent } from './booking-overview.component';

describe('BookingOverviewComponent', () => {
  let spectator: Spectator<BookingOverviewComponent>;

  const createVehicleOverviewComponent: SpectatorFactory<BookingOverviewComponent> =
    createComponentFactory({
      component: BookingOverviewComponent,
      shallow: true,
      declarations: [MockPipe(TranslatePipe)],
      providers: [mockProvider(BookingFacade)],
    });

  beforeEach(async () => {
    spectator = createVehicleOverviewComponent();
  });

  it('creates the BookingOverviewComponent', () => {
    expect(spectator.component).toBeTruthy();
  });

  it('loads all bookings on init', () => {
    const facade: SpyObject<BookingFacade> = spectator.inject(BookingFacade);

    expect(facade.getBookings).toHaveBeenCalled();
  });
});

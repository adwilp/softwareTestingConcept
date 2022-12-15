import {
  createComponentFactory,
  mockProvider,
  Spectator,
  SpectatorFactory,
} from '@ngneat/spectator';
import { TranslatePipe } from '@ngx-translate/core';
import { MockPipe } from 'ng-mocks';
import { BookingFacade } from '../_store/booking.facade';
import { BookingOverviewComponent } from './booking-overview.component';

describe('BookingOverviewComponent', () => {
  let spectator: Spectator<BookingOverviewComponent>;

  const createBokingOverviewComponent: SpectatorFactory<BookingOverviewComponent> =
    createComponentFactory({
      component: BookingOverviewComponent,
      shallow: true,
      declarations: [MockPipe(TranslatePipe)],
      providers: [mockProvider(BookingFacade)],
    });

  beforeEach(async () => {
    spectator = createBokingOverviewComponent();
  });

  it('creates booking overview component', () => {
    expect(spectator.component).toBeTruthy();
  });
});

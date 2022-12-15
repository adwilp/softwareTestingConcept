import {
  createComponentFactory,
  Spectator,
  SpectatorFactory,
} from '@ngneat/spectator';
import { BookingOverviewComponent } from './booking-overview.component';

describe('BookingOverviewComponent', () => {
  let spectator: Spectator<BookingOverviewComponent>;

  const createBokingOverviewComponent: SpectatorFactory<BookingOverviewComponent> =
    createComponentFactory({
      component: BookingOverviewComponent,
      shallow: true,
    });

  beforeEach(async () => {
    spectator = createBokingOverviewComponent();
  });

  it('creates booking overview component', () => {
    expect(spectator.component).toBeTruthy();
  });
});

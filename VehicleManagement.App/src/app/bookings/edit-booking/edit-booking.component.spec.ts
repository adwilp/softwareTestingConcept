import {
  createComponentFactory,
  mockProvider,
  Spectator,
  SpectatorFactory,
} from '@ngneat/spectator';
import { TranslatePipe } from '@ngx-translate/core';
import { MockPipe } from 'ng-mocks';
import { BookingFacade } from '../_store/booking.facade';
import { EditBookingComponent } from './edit-booking.component';

describe('EditBookingComponent', () => {
  let spectator: Spectator<EditBookingComponent>;

  const createEditBookingComponent: SpectatorFactory<EditBookingComponent> =
    createComponentFactory({
      component: EditBookingComponent,
      shallow: true,
      declarations: [MockPipe(TranslatePipe)],
      providers: [mockProvider(BookingFacade)],
    });

  beforeEach(async () => {
    spectator = createEditBookingComponent();
  });

  it('should create', () => {
    expect(spectator.component).toBeTruthy();
  });
});

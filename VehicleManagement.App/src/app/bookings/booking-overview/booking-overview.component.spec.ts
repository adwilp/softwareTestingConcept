import { Router } from '@angular/router';
import {
  byTestId,
  createComponentFactory,
  mockProvider,
  Spectator,
  SpectatorFactory,
  SpyObject,
} from '@ngneat/spectator';
import { TranslatePipe } from '@ngx-translate/core';
import { MockPipe } from 'ng-mocks';
import { BookingFacade } from '../_store/booking.facade';
import { flatBookings } from '../_store/booking.test-data';
import { BookingOverviewComponent } from './booking-overview.component';
import Spy = jasmine.Spy;

describe('BookingOverviewComponent', () => {
  let spectator: Spectator<BookingOverviewComponent>;

  const createBookingOverviewComponent: SpectatorFactory<BookingOverviewComponent> =
    createComponentFactory({
      component: BookingOverviewComponent,
      shallow: true,
      declarations: [MockPipe(TranslatePipe)],
      providers: [mockProvider(BookingFacade), mockProvider(Router)],
    });

  beforeEach(async () => {
    spectator = createBookingOverviewComponent();
  });

  it('creates the BookingOverviewComponent', () => {
    expect(spectator.component).toBeTruthy();
  });

  it('loads all bookings on init', () => {
    const facade: SpyObject<BookingFacade> = spectator.inject(BookingFacade);

    expect(facade.getBookings).toHaveBeenCalled();
  });

  describe('UI interactions', () => {
    it('calls navigateToAddBooking on add button click', () => {
      // ARRANGE
      const navigateToAddBooking: Spy = spyOn(
        spectator.component,
        'navigateToAddBooking'
      );

      // ACT
      spectator.click(byTestId('add-booking-button'));
      spectator.detectChanges();

      // ASSERT
      expect(navigateToAddBooking).toHaveBeenCalled();
    });
  });

  describe('router actions', () => {
    let router: SpyObject<Router>;

    beforeEach(() => {
      router = spectator.inject(Router);
    });

    it('calls router navigate on navigateToAddBooking', () => {
      // ACT
      spectator.component.navigateToAddBooking();

      // ASSERT
      expect(router.navigate).toHaveBeenCalledWith(['bookings', 'add']);
    });

    it('calls router navigate on navigateToEditBooking', () => {
      // ACT
      spectator.component.navigateToEditBooking({
        ...flatBookings[0],
        id: 12,
      });

      // ASSERT
      expect(router.navigate).toHaveBeenCalledWith(['bookings', 'edit', 12]);
    });
  });
});

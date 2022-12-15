import { Router } from '@angular/router';
import {
  byTestId,
  createComponentFactory,
  mockProvider,
  Spectator,
  SpectatorFactory,
  SpyObject,
} from '@ngneat/spectator';
import { DashboardComponent } from './dashboard.component';
import Spy = jasmine.Spy;

describe('DashboardComponent', () => {
  let spectator: Spectator<DashboardComponent>;

  const createDashboardComponent: SpectatorFactory<DashboardComponent> =
    createComponentFactory({
      component: DashboardComponent,
      shallow: true,
      providers: [mockProvider(Router)],
    });

  beforeEach(async () => {
    spectator = createDashboardComponent();
  });

  it('creates the DashboardComponent', () => {
    expect(spectator.component).toBeTruthy();
  });

  it('renders two app tiles', () => {
    // ASSERT
    expect(spectator.queryAll('app-tile').length).toBe(2);
  });

  describe('UI interactions', () => {
    it('calls navigateToBookings on booking tile click', () => {
      // ARRANGE
      const navigateToBookings: Spy = spyOn(
        spectator.component,
        'navigateToBookings'
      );

      // ACT
      spectator.click(byTestId('booking-tile'));
      spectator.detectChanges();

      // ASSERT
      expect(navigateToBookings).toHaveBeenCalled();
    });

    it('calls navigateToVehicles on vehicle tile click', () => {
      // ARRANGE
      const navigateToVehicles: Spy = spyOn(
        spectator.component,
        'navigateToVehicles'
      );

      // ACT
      spectator.click(byTestId('vehicle-tile'));
      spectator.detectChanges();

      // ASSERT
      expect(navigateToVehicles).toHaveBeenCalled();
    });
  });

  describe('router actions', () => {
    let router: SpyObject<Router>;

    beforeEach(() => {
      router = spectator.inject(Router);
    });

    it('calls router navigate on navigateToVehicles', () => {
      // ACT
      spectator.component.navigateToBookings();

      // ASSERT
      expect(router.navigate).toHaveBeenCalledWith(['bookings']);
    });

    it('calls router navigate on navigateToVehicles', () => {
      // ACT
      spectator.component.navigateToVehicles();

      // ASSERT
      expect(router.navigate).toHaveBeenCalledWith(['vehicles']);
    });
  });
});

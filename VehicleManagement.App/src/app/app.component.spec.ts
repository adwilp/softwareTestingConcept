import { AppComponent } from './app.component';
import {
  byTestId,
  createComponentFactory,
  mockProvider,
  Spectator,
  SpectatorFactory,
  SpyObject,
} from '@ngneat/spectator';
import { Router } from '@angular/router';
import { MockComponents, MockPipe } from 'ng-mocks';
import { TranslatePipe } from '@ngx-translate/core';
import Spy = jasmine.Spy;
import { MatSidenav } from '@angular/material/sidenav';

describe('AppComponent', () => {
  let spectator: Spectator<AppComponent>;

  const createAppComponent: SpectatorFactory<AppComponent> =
    createComponentFactory({
      component: AppComponent,
      shallow: true,
      declarations: [MockPipe(TranslatePipe), MockComponents(MatSidenav)],
      providers: [mockProvider(Router)],
    });

  beforeEach(async () => {
    spectator = createAppComponent();
  });

  it('creates the AppComponent', () => {
    // ASSERT
    expect(spectator.component).toBeTruthy();
  });

  it('renders a routerOutlet', () => {
    // ASSERT
    expect(spectator.query('router-outlet')).toBeTruthy();
  });

  describe('UI interactions', () => {
    it('calls navigateToHome on home button click', () => {
      // ARRANGE
      const navigateToHome: Spy = spyOn(spectator.component, 'navigateToHome');

      // ACT
      spectator.click(byTestId('home-button'));
      spectator.detectChanges();

      // ASSERT
      expect(navigateToHome).toHaveBeenCalled();
    });

    it('calls navigateToVehicles on vehicle button click', () => {
      // ARRANGE
      const navigateToVehicles: Spy = spyOn(
        spectator.component,
        'navigateToVehicles'
      );

      // ACT
      spectator.click(byTestId('vehicle-button'));
      spectator.detectChanges();

      // ASSERT
      expect(navigateToVehicles).toHaveBeenCalled();
    });

    it('calls navigateToBookings on booking button click', () => {
      // ARRANGE
      const navigateToBookings: Spy = spyOn(
        spectator.component,
        'navigateToBookings'
      );

      // ACT
      spectator.click(byTestId('booking-button'));
      spectator.detectChanges();

      // ASSERT
      expect(navigateToBookings).toHaveBeenCalled();
    });
  });

  describe('router actions', () => {
    let router: SpyObject<Router>;

    beforeEach(() => {
      router = spectator.inject(Router);
    });

    it('calls router navigate on navigateToHome', () => {
      // ACT
      spectator.component.navigateToHome();

      // ASSERT
      expect(router.navigate).toHaveBeenCalledWith(['']);
    });

    it('calls router navigate on navigateToVehicles', () => {
      // ACT
      spectator.component.navigateToVehicles();

      // ASSERT
      expect(router.navigate).toHaveBeenCalledWith(['vehicles']);
    });

    it('calls router navigate on navigateToVehicles', () => {
      // ACT
      spectator.component.navigateToBookings();

      // ASSERT
      expect(router.navigate).toHaveBeenCalledWith(['bookings']);
    });
  });
});

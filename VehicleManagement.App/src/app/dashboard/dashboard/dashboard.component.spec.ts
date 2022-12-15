import { Router } from '@angular/router';
import {
  createComponentFactory,
  mockProvider,
  Spectator,
  SpectatorFactory,
  SpyObject,
} from '@ngneat/spectator';
import { DashboardComponent } from './dashboard.component';

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

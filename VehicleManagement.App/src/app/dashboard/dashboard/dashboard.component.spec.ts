import {
  createComponentFactory,
  Spectator,
  SpectatorFactory,
} from '@ngneat/spectator';
import { DashboardComponent } from './dashboard.component';

describe('DashboardComponent', () => {
  let spectator: Spectator<DashboardComponent>;

  const createDashboardComponent: SpectatorFactory<DashboardComponent> =
    createComponentFactory({
      component: DashboardComponent,
      shallow: true,
    });

  beforeEach(async () => {
    spectator = createDashboardComponent();
  });

  it('creates the DashboardComponent', () => {
    expect(spectator.component).toBeTruthy();
  });
});

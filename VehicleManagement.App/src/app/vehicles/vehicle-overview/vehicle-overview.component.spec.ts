import { createComponentFactory, mockProvider, Spectator, SpectatorFactory } from '@ngneat/spectator';
import { TranslatePipe } from '@ngx-translate/core';
import { MockPipe } from 'ng-mocks';
import { VehicleFacade } from '../_store/vehicle.facade';
import { VehicleOverviewComponent } from './vehicle-overview.component';

describe('VehicleOverviewComponent', () => {
  let spectator: Spectator<VehicleOverviewComponent>;

  const createVehicleOverviewComponent: SpectatorFactory<VehicleOverviewComponent> = createComponentFactory({
    component: VehicleOverviewComponent,
    shallow: true,
    declarations: [
      MockPipe(TranslatePipe)
    ],
    providers: [
      mockProvider(VehicleFacade)
    ]
  });

  beforeEach(async () => {
    spectator = createVehicleOverviewComponent();
  });

  it('creates the VehicleOverviewComponent', () => {
    expect(spectator.component).toBeTruthy();
  });

  it('loads all vehicles on init', () =>{
    const facade = spectator.inject(VehicleFacade);

    expect(facade.getVehicles).toHaveBeenCalled();
  });
});

import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { FlatVehicle } from '../models/flat-vehicle.model';
import { VehicleQuery } from './vehicle.selectors';
import * as VehicleActions from './vehicle.actions';

@Injectable({ providedIn: 'root' })
export class VehicleFacade {
  // VEHICLES
  readonly vehicles$: Observable<FlatVehicle[]> = this.store.select(
    VehicleQuery.selectVehicles
  );

  readonly vehiclesLoading$: Observable<boolean> = this.store.select(
    VehicleQuery.selectVehiclesLoading
  );

  constructor(private store: Store) {}

  getVehicles(): void {
    this.store.dispatch(VehicleActions.getVehicles());
  }
}

import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FlatVehicle } from '../models/flat-vehicle.model';
import { VehicleFacade } from '../_store/vehicle.facade';

@Component({
  selector: 'app-vehicle-overview',
  templateUrl: './vehicle-overview.component.html',
  styleUrls: ['./vehicle-overview.component.scss'],
})
export class VehicleOverviewComponent implements OnInit {
  get displayedColumns(): string[] {
    return ['fIN', 'licensePlate', 'color', 'mileage', 'manufacturer'];
  }

  get vehicles$(): Observable<FlatVehicle[]> {
    return this.vehicleFacade.vehicles$;
  }

  constructor(private vehicleFacade: VehicleFacade) {}

  ngOnInit(): void {
    this.vehicleFacade.getVehicles();
  }
}

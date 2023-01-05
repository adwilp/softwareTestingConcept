import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { FlatVehicle } from 'src/app/vehicles/models/flat-vehicle.model';
import { VehicleFacade } from 'src/app/vehicles/_store/vehicle.facade';

@Component({
  selector: 'app-add-booking',
  templateUrl: './add-booking.component.html',
  styleUrls: ['./add-booking.component.scss'],
})
export class AddBookingComponent implements OnInit {
  bookingForm: FormGroup;

  get vehicles$(): Observable<FlatVehicle[]> {
    return this.vehicleFacade.vehicles$;
  }

  constructor(private vehicleFacade: VehicleFacade) {}

  ngOnInit(): void {
    this.initBookingForm();
    this.vehicleFacade.getVehicles();
  }

  private initBookingForm(): void {
    this.bookingForm = new FormGroup({
      range: new FormGroup({
        start: new FormControl(),
        end: new FormControl(),
      }),
      employeeNumber: new FormControl(),
      fin: new FormControl(),
    });
  }
}

import { HttpErrorResponse } from '@angular/common/http';
import {
  HttpClientTestingModule,
  HttpTestingController,
  TestRequest,
} from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { environment } from 'src/environments/environment';
import { FlatVehicle } from './models/flat-vehicle.model';
import { VehicleService } from './vehicle.service';

const baseUrl: string = environment.baseUrl;

describe('VehicleService', () => {
  let vehicleService: VehicleService;
  let controller: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [VehicleService],
    });

    vehicleService = TestBed.inject(VehicleService);
    controller = TestBed.inject(HttpTestingController);
  });

  it('loads all vehicles', () => {
    // ARRANGE
    let currentVehicles: FlatVehicle[] | undefined;
    vehicleService.getVehicles().subscribe((vehicles: FlatVehicle[]) => {
      currentVehicles = vehicles;
    });

    // ACT
    const request: TestRequest = controller.expectOne({
      method: 'GET',
      url: `${baseUrl}Vehicles`
    });
    request.flush(flatVehicles);
    controller.verify();

    // ASSERT
    expect(currentVehicles).toEqual(flatVehicles);
  });

  it('passes through all vehicles errors', () => {
    // ARRANGE
    const status: number = 500;
    const statusText: string = 'Server error';
    const progressEvent: ProgressEvent = new ProgressEvent('API Error');

    let currentError: HttpErrorResponse | undefined;

    vehicleService.getVehicles().subscribe(
      () => {
        fail('next handler must not be called!');
      },
      (error: HttpErrorResponse) => {
        currentError = error;
      },
      () => {
        fail('complete handler must not be called!');
      }
    );

    // ACT
    const request = controller.expectOne({
      method: 'GET',
      url: `${baseUrl}Vehicles`
    });
    request.error(progressEvent, {status, statusText});

    // ASSERT
    if (!currentError) {
      fail('error response must not be undefined!');
    }

    expect(currentError.error).toBe(progressEvent);
    expect(currentError.status).toBe(status);
    expect(currentError.statusText).toBe(statusText);
  });
});

const flatVehicles: FlatVehicle[] = [
  {
    fin: '1234567890',
    licensePlate: 'VEC-ZZ-123',
    color: 'yellow',
    mileage: 12.1,
    manufacturer: 'Audi',
  },
  {
    fin: 'ABCDEFG456987',
    licensePlate: 'MI-XY-1234',
    color: 'yellow',
    mileage: 12.1,
    manufacturer: 'Opel',
  },
];

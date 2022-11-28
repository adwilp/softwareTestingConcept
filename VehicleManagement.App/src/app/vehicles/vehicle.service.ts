import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FlatVehicle } from './models/flat-vehicle.model';

@Injectable({ providedIn: 'root' })
export class VehicleService {
  private _baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getVehicles(): Observable<FlatVehicle[]> {
    return this.http.get<FlatVehicle[]>(`${this._baseUrl}Vehicles`);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Booking } from './models/booking.model';
import { FlatBooking } from './models/flat-booking.model';
import { UpdateableBooking } from './models/updateable-booking.model';

@Injectable({ providedIn: 'root' })
export class BookingService {
  private _baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getBookings(): Observable<FlatBooking[]> {
    return this.http.get<FlatBooking[]>(`${this._baseUrl}Bookings`);
  }

  getBooking(id: number): Observable<UpdateableBooking> {
    return this.http.get<UpdateableBooking>(`${this._baseUrl}Bookings/${id}`);
  }

  addBooking(booking: Booking): Observable<FlatBooking> {
    return this.http.post<FlatBooking>(`${this._baseUrl}Bookings`, booking);
  }

  editBooking(booking: UpdateableBooking): Observable<FlatBooking> {
    return this.http.put<FlatBooking>(`${this._baseUrl}Bookings`, booking);
  }
}

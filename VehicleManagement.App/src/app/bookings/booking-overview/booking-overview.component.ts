import { Component, OnInit } from '@angular/core';
import { FlatBooking } from '../models/flat-booking.model';
import { BookingFacade } from '../_store/booking.facade';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-booking-overview',
  templateUrl: './booking-overview.component.html',
  styleUrls: ['./booking-overview.component.scss'],
})
export class BookingOverviewComponent implements OnInit {
  get displayedColumns(): string[] {
    return [
      'id',
      'start',
      'end',
      'employeeNumber',
      'fin',
      'licensePlate',
      'action',
    ];
  }

  get vehicles$(): Observable<FlatBooking[]> {
    return this.bookingFacade.bookings$;
  }

  constructor(private bookingFacade: BookingFacade, private router: Router) {}

  ngOnInit(): void {
    this.bookingFacade.getBookings();
  }

  navigateToAddBooking(): void {
    this.router.navigate(['bookings', 'add']);
  }

  navigateToEditBooking(booking: FlatBooking): void {
    this.router.navigate(['bookings', 'edit', booking.id]);
  }
}

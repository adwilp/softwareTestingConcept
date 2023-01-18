import { Component, OnInit } from '@angular/core';
import { Booking } from '../models/booking.model';
import { UpdateableBooking } from '../models/updateable-booking.model';
import { BookingFacade } from '../_store/booking.facade';
import { Observable, take } from 'rxjs';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-edit-booking',
  templateUrl: './edit-booking.component.html',
  styleUrls: ['./edit-booking.component.scss'],
})
export class EditBookingComponent implements OnInit {
  public get booking(): Observable<UpdateableBooking> {
    return this.bookingFacade.booking$;
  }

  constructor(
    private bookingFacade: BookingFacade,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params?.pipe(take(1)).subscribe((params: Params) => {
      this.bookingFacade.getBooking(params['id']);
    });
  }

  bookingSubmited(booking: Booking): void {
    this.bookingFacade.booking$
      ?.pipe(take(1))
      .subscribe((selectedBooking: UpdateableBooking) => {
        const update: UpdateableBooking = {
          ...booking,
          id: selectedBooking.id,
        };

        this.bookingFacade.editBooking(update);
      });
  }
}

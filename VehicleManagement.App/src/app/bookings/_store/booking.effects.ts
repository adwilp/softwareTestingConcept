import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap } from 'rxjs';
import * as BookingActions from './booking.actions';
import { FlatBooking } from '../models/flat-booking.model';
import { BookingService } from '../booking.service';

@Injectable()
export class BookingEffects {
  // eslint-disable-next-line @typescript-eslint/typedef
  getBookings$ = createEffect(() => {
    return this.action$.pipe(
      ofType(BookingActions.getBookings),
      switchMap(() => {
        return this.bookingService.getBookings().pipe(
          map((bookings: FlatBooking[]) => {
            return BookingActions.getBookingsSuccess({
              bookings: bookings,
            });
          }),
          catchError(() => {
            //TODO AK: Replace with correct action
            return of(
              BookingActions.getBookingsSuccess({
                bookings: null,
              })
            );
          })
        );
      })
    );
  });

  constructor(
    private action$: Actions,
    private bookingService: BookingService
  ) {}
}

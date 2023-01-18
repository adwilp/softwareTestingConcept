import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap, tap } from 'rxjs';
import * as BookingActions from './booking.actions';
import { FlatBooking } from '../models/flat-booking.model';
import { BookingService } from '../booking.service';
import { Router } from '@angular/router';
import { UpdateableBooking } from '../models/updateable-booking.model';

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

  // eslint-disable-next-line @typescript-eslint/typedef
  addBooking$ = createEffect(() => {
    return this.action$.pipe(
      ofType(BookingActions.addBooking),
      switchMap((value: BookingActions.addBooking) => {
        return this.bookingService.addBooking(value.booking).pipe(
          map((booking: FlatBooking) => {
            return BookingActions.addBookingSuccess({ booking: booking });
          }),
          catchError(() => {
            // TODO AK: Replace with correct action
            return of(BookingActions.addBookingSuccess({ booking: null }));
          })
        );
      })
    );
  });

  // eslint-disable-next-line @typescript-eslint/typedef
  addBookingSuccess$ = createEffect(
    () => {
      return this.action$.pipe(
        ofType(BookingActions.addBookingSuccess),
        tap(() => {
          this.router.navigate(['bookings']);
        })
      );
    },
    { dispatch: false }
  );

  // eslint-disable-next-line @typescript-eslint/typedef
  getSelectedBooking$ = createEffect(() => {
    return this.action$.pipe(
      ofType(BookingActions.getBooking),
      switchMap((value: BookingActions.getBooking) => {
        return this.bookingService.getBooking(value.id).pipe(
          map((booking: UpdateableBooking) => {
            return BookingActions.getBookingSuccess({
              booking: booking,
            });
          }),
          catchError(() => {
            //TODO AK: Replace with correct action
            return of(
              BookingActions.getBookingSuccess({
                booking: null,
              })
            );
          })
        );
      })
    );
  });

  // eslint-disable-next-line @typescript-eslint/typedef
  editBooking$ = createEffect(() => {
    return this.action$.pipe(
      ofType(BookingActions.editBooking),
      switchMap((value: BookingActions.editBooking) => {
        return this.bookingService.editBooking(value.booking).pipe(
          map((booking: FlatBooking) => {
            return BookingActions.editBookingSuccess({ booking: booking });
          }),
          catchError(() => {
            // TODO AK: Replace with correct action
            return of(BookingActions.editBookingSuccess({ booking: null }));
          })
        );
      })
    );
  });

  // eslint-disable-next-line @typescript-eslint/typedef
  editBookingSuccess$ = createEffect(
    () => {
      return this.action$.pipe(
        ofType(BookingActions.editBookingSuccess),
        tap(() => {
          this.router.navigate(['bookings']);
        })
      );
    },
    { dispatch: false }
  );

  constructor(
    private action$: Actions,
    private bookingService: BookingService,
    private router: Router
  ) {}
}

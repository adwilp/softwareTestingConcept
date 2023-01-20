import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, of, switchMap, tap } from 'rxjs';
import * as BookingActions from './booking.actions';
import * as AppActions from '../../_store/app-actions';
import { FlatBooking } from '../models/flat-booking.model';
import { BookingService } from '../booking.service';
import { Router } from '@angular/router';
import { UpdateableBooking } from '../models/updateable-booking.model';
import { HttpErrorResponse } from '@angular/common/http';

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
          catchError((error: HttpErrorResponse) => {
            return of(
              AppActions.appError({
                message: 'Error loading all bookings!',
                error: error,
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
          catchError((error: HttpErrorResponse) => {
            return of(
              AppActions.appError({
                message: 'Error adding booking!',
                error: error,
              })
            );
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
          catchError((error: HttpErrorResponse) => {
            return of(
              AppActions.appError({
                message: 'Error loading selected booking!',
                error: error,
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
          catchError((error: HttpErrorResponse) => {
            return of(
              AppActions.appError({
                message: 'Error editing booking!',
                error: error,
              })
            );
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

  // eslint-disable-next-line @typescript-eslint/typedef
  deleteBooking$ = createEffect(() => {
    return this.action$.pipe(
      ofType(BookingActions.deleteBooking),
      switchMap((value: BookingActions.deleteBooking) => {
        return this.bookingService.deleteBooking(value.id).pipe(
          map(() => {
            return BookingActions.deleteBookingSuccess();
          }),
          catchError((error: HttpErrorResponse) => {
            return of(
              AppActions.appError({
                message: 'Error deleting booking!',
                error: error,
              })
            );
          })
        );
      })
    );
  });

  //eslint-disable-next-line @typescript-eslint/typedef
  deleteBookingSuccess$ = createEffect(() => {
    return this.action$.pipe(
      ofType(BookingActions.deleteBookingSuccess),
      map(() => {
        return BookingActions.getBookings();
      })
    );
  });

  constructor(
    private action$: Actions,
    private bookingService: BookingService,
    private router: Router
  ) {}
}

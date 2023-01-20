import { TestBed } from '@angular/core/testing';
import { Observable, of, throwError } from 'rxjs';
import { provideMockActions } from '@ngrx/effects/testing';
import { BookingEffects } from './booking.effects';
import { bookingReducer } from './booking.reducers';
import { BookingService } from '../booking.service';
import { MockProviders } from 'ng-mocks';
import * as BookingActions from './booking.actions';
import * as AppActions from '../../_store/app-actions';
import Spy = jasmine.Spy;
import { HttpErrorResponse } from '@angular/common/http';
import { flatBookings, booking, updateableBooking } from './booking.test-data';
import { TypedAction } from '@ngrx/store/src/models';
import { Router } from '@angular/router';

describe('BookingEffects', () => {
  type getBookingsAction = TypedAction<'[Bookings] Get bookings'>;

  type appErrorAction = AppActions.appError &
    TypedAction<'[App] Error occured'>;

  type getBookingsSuccessAction = BookingActions.getBookingsSuccess &
    TypedAction<'[Bookings] Get bookings - Success'>;

  type getBookingSuccessAction = BookingActions.getBookingSuccess &
    TypedAction<'[Bookings] Get booking - Success'>;

  type addBookingSuccessAction = BookingActions.addBookingSuccess &
    TypedAction<'[Bookings] Add booking - Success'>;

  type editBookingSuccessAction = BookingActions.editBookingSuccess &
    TypedAction<'[Bookings] Edit booking - Success'>;

  type deleteBookingSuccessAction =
    TypedAction<'[Bookings] Delete booking - Success'>;

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  let actionsMock$: Observable<any>;
  let effects: BookingEffects;
  let bookingService: BookingService;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideMockActions(() => actionsMock$),
        BookingEffects,
        MockProviders(bookingReducer, BookingService, Router),
      ],
    });

    bookingService = TestBed.inject(BookingService);
    effects = TestBed.inject(BookingEffects);
    router = TestBed.inject(Router);
  });

  describe('getBookings$', () => {
    let bookingSpy: Spy;

    beforeEach(() => {
      actionsMock$ = of(BookingActions.getBookings);
      bookingSpy = spyOn(bookingService, 'getBookings');
    });

    it('calls getBookings once', () => {
      // ARRANGE
      bookingSpy.and.returnValue(of(flatBookings));

      // ACT
      effects.getBookings$.subscribe();

      // ASSERT
      expect(bookingSpy).toHaveBeenCalledOnceWith();
    });

    it('returns a stream with FlatBookings', () => {
      // ARRANGE
      let currentOutcome: getBookingsSuccessAction | undefined;
      const outcome: getBookingsSuccessAction =
        BookingActions.getBookingsSuccess({
          bookings: flatBookings,
        });
      bookingSpy.and.returnValue(of(flatBookings));

      // ACT
      effects.getBookings$.subscribe((action: getBookingsSuccessAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        bookings: outcome.bookings,
      });
    });

    it('returns a stream with app error action on http error', () => {
      // ARRANGE
      let currentOutcome: appErrorAction | undefined;
      const error: HttpErrorResponse = new HttpErrorResponse({});
      const outcome: appErrorAction = AppActions.appError({
        message: 'Error loading all bookings!',
        error: error,
      });
      bookingSpy.and.callFake(() => {
        return throwError(() => error);
      });

      // ACT
      effects.getBookings$.subscribe((action: appErrorAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        message: outcome.message,
        error: outcome.error,
      });
    });
  });

  describe('getSelectedBooking', () => {
    let bookingSpy: Spy;

    beforeEach(() => {
      actionsMock$ = of(BookingActions.getBooking({ id: 1 }));
      bookingSpy = spyOn(bookingService, 'getBooking');
    });

    it('calls getBooking once', () => {
      // ARRANGE
      bookingSpy.and.returnValue(of(updateableBooking));

      // ACT
      effects.getSelectedBooking$.subscribe();

      // ASSERT
      expect(bookingSpy).toHaveBeenCalledOnceWith(1);
    });

    it('returns a stream with UpdateableBooking', () => {
      // ARRANGE
      let currentOutcome: getBookingSuccessAction | undefined;
      const outcome: getBookingSuccessAction = BookingActions.getBookingSuccess(
        {
          booking: updateableBooking,
        }
      );
      bookingSpy.and.returnValue(of(updateableBooking));

      // ACT
      effects.getSelectedBooking$.subscribe(
        (action: getBookingSuccessAction) => {
          currentOutcome = action;
        }
      );

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        booking: outcome.booking,
      });
    });

    it('returns a stream with app error action on http error', () => {
      // ARRANGE
      let currentOutcome: appErrorAction | undefined;
      const error: HttpErrorResponse = new HttpErrorResponse({});
      const outcome: appErrorAction = AppActions.appError({
        message: 'Error loading selected booking!',
        error: error,
      });
      bookingSpy.and.callFake(() => {
        return throwError(() => error);
      });

      // ACT
      effects.getSelectedBooking$.subscribe((action: appErrorAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        message: outcome.message,
        error: outcome.error,
      });
    });
  });

  describe('addBooking$', () => {
    let bookingServiceSpy: Spy;

    beforeEach(() => {
      actionsMock$ = of(BookingActions.addBooking({ booking: booking }));
      bookingServiceSpy = spyOn(bookingService, 'addBooking');
    });

    it('calls addBooking once', () => {
      // ARRANGE
      bookingServiceSpy.and.returnValue(of(flatBookings[0]));

      // ACT
      effects.addBooking$.subscribe();

      // ASSERT
      expect(bookingServiceSpy).toHaveBeenCalledOnceWith(booking);
    });

    it('returns a stream with FlatBooking', () => {
      // ARRANGE
      let currentOutcome: addBookingSuccessAction | undefined;
      const outcome: addBookingSuccessAction = BookingActions.addBookingSuccess(
        {
          booking: flatBookings[0],
        }
      );
      bookingServiceSpy.and.returnValue(of(flatBookings[0]));

      // ACT
      effects.addBooking$.subscribe((action: addBookingSuccessAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        booking: outcome.booking,
      });
    });

    it('returns a stream with app error action on http error', () => {
      // ARRANGE
      let currentOutcome: appErrorAction | undefined;
      const error: HttpErrorResponse = new HttpErrorResponse({});
      const outcome: appErrorAction = AppActions.appError({
        message: 'Error adding booking!',
        error: error,
      });
      bookingServiceSpy.and.callFake(() => {
        return throwError(() => error);
      });

      // ACT
      effects.addBooking$.subscribe((action: appErrorAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        message: outcome.message,
        error: outcome.error,
      });
    });
  });

  describe('addBookingSuccess$', () => {
    let routerSpy: Spy;

    beforeEach(() => {
      actionsMock$ = of(
        BookingActions.addBookingSuccess({ booking: flatBookings[0] })
      );
      routerSpy = spyOn(router, 'navigate');
    });

    it('calls navigate on success', () => {
      // ACT
      effects.addBookingSuccess$.subscribe();

      // ASSERT
      expect(routerSpy).toHaveBeenCalledOnceWith(['bookings']);
    });
  });

  describe('editBooking$', () => {
    let bookingServiceSpy: Spy;

    beforeEach(() => {
      actionsMock$ = of(
        BookingActions.editBooking({ booking: updateableBooking })
      );
      bookingServiceSpy = spyOn(bookingService, 'editBooking');
    });

    it('calls editBooking once', () => {
      // ARRANGE
      bookingServiceSpy.and.returnValue(
        of({ ...flatBookings[0], id: updateableBooking.id })
      );

      // ACT
      effects.editBooking$.subscribe();

      // ASSERT
      expect(bookingServiceSpy).toHaveBeenCalledOnceWith(updateableBooking);
    });

    it('returns a stream with FlatBooking', () => {
      // ARRANGE
      let currentOutcome: editBookingSuccessAction | undefined;
      const outcome: editBookingSuccessAction =
        BookingActions.editBookingSuccess({
          booking: flatBookings[0],
        });
      bookingServiceSpy.and.returnValue(of(flatBookings[0]));

      // ACT
      effects.editBooking$.subscribe((action: editBookingSuccessAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        booking: outcome.booking,
      });
    });

    it('returns a stream with app error action on http error', () => {
      // ARRANGE
      let currentOutcome: appErrorAction | undefined;
      const error: HttpErrorResponse = new HttpErrorResponse({});
      const outcome: appErrorAction = AppActions.appError({
        message: 'Error editing booking!',
        error: error,
      });
      bookingServiceSpy.and.callFake(() => {
        return throwError(() => error);
      });

      // ACT
      effects.editBooking$.subscribe((action: appErrorAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        message: outcome.message,
        error: outcome.error,
      });
    });
  });

  describe('editBookingSuccess$', () => {
    let routerSpy: Spy;

    beforeEach(() => {
      actionsMock$ = of(
        BookingActions.editBookingSuccess({ booking: flatBookings[0] })
      );
      routerSpy = spyOn(router, 'navigate');
    });

    it('calls navigate on success', () => {
      // ACT
      effects.editBookingSuccess$.subscribe();

      // ASSERT
      expect(routerSpy).toHaveBeenCalledOnceWith(['bookings']);
    });
  });

  describe('deleteBooking$', () => {
    let bookingServiceSpy: Spy;

    beforeEach(() => {
      actionsMock$ = of(BookingActions.deleteBooking({ id: 12 }));
      bookingServiceSpy = spyOn(bookingService, 'deleteBooking');
    });

    it('calls editBooking once', () => {
      // ARRANGE
      bookingServiceSpy.and.returnValue(of());

      // ACT
      effects.deleteBooking$.subscribe();

      // ASSERT
      expect(bookingServiceSpy).toHaveBeenCalledOnceWith(12);
    });

    it('returns a stream', () => {
      // ARRANGE
      let currentOutcome: deleteBookingSuccessAction | undefined;
      const outcome: deleteBookingSuccessAction =
        BookingActions.deleteBookingSuccess();
      bookingServiceSpy.and.returnValue(of(flatBookings[0]));

      // ACT
      effects.deleteBooking$.subscribe((action: deleteBookingSuccessAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
      });
    });

    it('returns a stream with app error action http error', () => {
      // ARRANGE
      let currentOutcome: appErrorAction | undefined;
      const error: HttpErrorResponse = new HttpErrorResponse({});
      const outcome: appErrorAction = AppActions.appError({
        message: 'Error deleting booking!',
        error: error,
      });
      bookingServiceSpy.and.callFake(() => {
        return throwError(() => error);
      });

      // ACT
      effects.deleteBooking$.subscribe((action: appErrorAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        message: outcome.message,
        error: outcome.error,
      });
    });
  });

  describe('deleteBookingSuccess$', () => {
    beforeEach(() => {
      actionsMock$ = of(BookingActions.deleteBookingSuccess());
    });

    it('returns getBookings action', () => {
      let currentOutcome: getBookingsAction | undefined;
      const outcome: getBookingsAction = BookingActions.getBookings();

      // ACT
      effects.deleteBookingSuccess$.subscribe((action: getBookingsAction) => {
        currentOutcome = action;
      });

      // ASSERT
      expect(currentOutcome).toEqual({
        type: outcome.type,
      });
    });
  });
});

import { TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { Observable, of } from 'rxjs';
import { AppEffects } from './app.effects';
import * as AppActions from './app-actions';
import { HttpErrorResponse } from '@angular/common/http';
import Spy = jasmine.Spy;

describe('AppEffects', () => {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  let actionsMock$: Observable<any>;
  let effects: AppEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideMockActions(() => actionsMock$), AppEffects],
    });

    effects = TestBed.inject(AppEffects);
  });

  describe('appError', () => {
    const errorMessage: string = 'This is a testing error!';
    const error: HttpErrorResponse = new HttpErrorResponse({});

    beforeEach(() => {
      actionsMock$ = of(
        AppActions.appError({
          message: errorMessage,
          error: error,
        })
      );
    });

    it('', () => {
      // ARRANGE
      const consoleErrorSpy: Spy = spyOn(console, 'error');
      const consoleDirSpy: Spy = spyOn(console, 'dir');

      // ACT
      effects.appError$.subscribe();

      // ASSERT
      expect(consoleErrorSpy).toHaveBeenCalledOnceWith(errorMessage);
      expect(consoleDirSpy).toHaveBeenCalledOnceWith(error);
    });
  });
});

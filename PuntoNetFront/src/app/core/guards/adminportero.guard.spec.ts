import { TestBed, async, inject } from '@angular/core/testing';

import { AdminporteroGuard } from './adminportero.guard';

describe('AdminporteroGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminporteroGuard]
    });
  });

  it('should ...', inject([AdminporteroGuard], (guard: AdminporteroGuard) => {
    expect(guard).toBeTruthy();
  }));
});

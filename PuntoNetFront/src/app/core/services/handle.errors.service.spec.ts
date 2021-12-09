import { TestBed } from '@angular/core/testing';

import { Handle.ErrorsService } from './handle.errors.service';

describe('Handle.ErrorsService', () => {
  let service: Handle.ErrorsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Handle.ErrorsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

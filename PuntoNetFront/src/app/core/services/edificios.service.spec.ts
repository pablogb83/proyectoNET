import { TestBed } from '@angular/core/testing';

import { EdificiosService } from './edificios.service';

describe('EdificiosService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EdificiosService = TestBed.get(EdificiosService);
    expect(service).toBeTruthy();
  });
});

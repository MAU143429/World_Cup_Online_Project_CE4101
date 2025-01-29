import { TestBed } from '@angular/core/testing';

import { WcoService } from './wco.service';

describe('WcoService', () => {
  let service: WcoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WcoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { EnvUrlService } from './env-url.service';

describe('EnvUrlService', () => {
  let service: EnvUrlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnvUrlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

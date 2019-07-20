import { TestBed } from '@angular/core/testing';

import { BaseHubService } from './base-hub.service';

describe('BaseHubService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BaseHubService = TestBed.get(BaseHubService);
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { MatchHubService } from './match-hub.service';

describe('MatchHubService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MatchHubService = TestBed.get(MatchHubService);
    expect(service).toBeTruthy();
  });
});

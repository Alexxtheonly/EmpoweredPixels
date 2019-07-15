import { TestBed, inject } from '@angular/core/testing';

import { MatchService as MatchService } from './match.service';

describe('MatchServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MatchService]
    });
  });

  it('should be created', inject([MatchService], (service: MatchService) => {
    expect(service).toBeTruthy();
  }));
});

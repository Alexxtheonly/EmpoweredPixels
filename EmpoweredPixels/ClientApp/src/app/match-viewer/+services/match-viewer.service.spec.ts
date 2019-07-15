import { TestBed, inject } from '@angular/core/testing';

import { MatchViewerService } from './match-viewer.service';

describe('MatchViewerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MatchViewerService]
    });
  });

  it('should be created', inject([MatchViewerService], (service: MatchViewerService) => {
    expect(service).toBeTruthy();
  }));
});

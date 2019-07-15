import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchParticipantComponent } from './match-participant.component';

describe('MatchParticipantComponent', () => {
  let component: MatchParticipantComponent;
  let fixture: ComponentFixture<MatchParticipantComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatchParticipantComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchParticipantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

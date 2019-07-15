import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchCreationComponent } from './match-creation.component';

describe('MatchCreationComponent', () => {
  let component: MatchCreationComponent;
  let fixture: ComponentFixture<MatchCreationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatchCreationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

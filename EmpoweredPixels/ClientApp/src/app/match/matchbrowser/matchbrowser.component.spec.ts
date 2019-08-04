import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchbrowserComponent } from './matchbrowser.component';

describe('MatchbrowserComponent', () => {
  let component: MatchbrowserComponent;
  let fixture: ComponentFixture<MatchbrowserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatchbrowserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchbrowserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

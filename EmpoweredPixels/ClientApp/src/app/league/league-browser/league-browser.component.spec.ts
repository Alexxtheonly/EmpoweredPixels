import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeagueBrowserComponent } from './league-browser.component';

describe('LeagueBrowserComponent', () => {
  let component: LeagueBrowserComponent;
  let fixture: ComponentFixture<LeagueBrowserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeagueBrowserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeagueBrowserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

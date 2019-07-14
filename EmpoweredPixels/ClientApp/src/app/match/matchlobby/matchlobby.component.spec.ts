import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchlobbyComponent } from './matchlobby.component';

describe('MatchlobbyComponent', () => {
  let component: MatchlobbyComponent;
  let fixture: ComponentFixture<MatchlobbyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatchlobbyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchlobbyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

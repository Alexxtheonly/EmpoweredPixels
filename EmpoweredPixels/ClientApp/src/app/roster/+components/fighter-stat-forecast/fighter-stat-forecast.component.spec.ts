import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FighterStatForecastComponent } from './fighter-stat-forecast.component';

describe('FighterStatForecastComponent', () => {
  let component: FighterStatForecastComponent;
  let fixture: ComponentFixture<FighterStatForecastComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FighterStatForecastComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FighterStatForecastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

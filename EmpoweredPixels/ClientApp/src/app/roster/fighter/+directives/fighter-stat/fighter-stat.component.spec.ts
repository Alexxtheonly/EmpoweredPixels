import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FighterStatComponent } from './fighter-stat.component';

describe('FighterStatComponent', () => {
  let component: FighterStatComponent;
  let fixture: ComponentFixture<FighterStatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FighterStatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FighterStatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

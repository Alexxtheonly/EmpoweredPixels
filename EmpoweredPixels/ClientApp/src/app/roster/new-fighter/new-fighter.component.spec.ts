import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewFighterComponent } from './new-fighter.component';

describe('NewFighterComponent', () => {
  let component: NewFighterComponent;
  let fixture: ComponentFixture<NewFighterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewFighterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewFighterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

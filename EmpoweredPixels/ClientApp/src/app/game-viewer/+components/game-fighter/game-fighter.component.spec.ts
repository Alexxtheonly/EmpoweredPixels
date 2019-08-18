import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GameFighterComponent } from './game-fighter.component';

describe('GameFighterComponent', () => {
  let component: GameFighterComponent;
  let fixture: ComponentFixture<GameFighterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GameFighterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameFighterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchLogComponent } from './match-log.component';

describe('MatchLogComponent', () => {
  let component: MatchLogComponent;
  let fixture: ComponentFixture<MatchLogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatchLogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RewardContentComponent } from './reward-content.component';

describe('RewardContentComponent', () => {
  let component: RewardContentComponent;
  let fixture: ComponentFixture<RewardContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RewardContentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RewardContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

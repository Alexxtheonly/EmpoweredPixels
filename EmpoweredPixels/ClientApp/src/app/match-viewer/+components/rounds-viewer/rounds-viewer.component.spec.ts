import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoundsViewerComponent } from './rounds-viewer.component';

describe('RoundsViewerComponent', () => {
  let component: RoundsViewerComponent;
  let fixture: ComponentFixture<RoundsViewerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoundsViewerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoundsViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

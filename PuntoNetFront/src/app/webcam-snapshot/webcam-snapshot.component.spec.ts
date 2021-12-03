import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WebcamSnapshotComponent } from './webcam-snapshot.component';

describe('WebcamSnapshotComponent', () => {
  let component: WebcamSnapshotComponent;
  let fixture: ComponentFixture<WebcamSnapshotComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WebcamSnapshotComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WebcamSnapshotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

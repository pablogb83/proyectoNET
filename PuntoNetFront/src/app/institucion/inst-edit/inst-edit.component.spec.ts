import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InstEditComponent } from './inst-edit.component';

describe('InstEditComponent', () => {
  let component: InstEditComponent;
  let fixture: ComponentFixture<InstEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InstEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InstEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

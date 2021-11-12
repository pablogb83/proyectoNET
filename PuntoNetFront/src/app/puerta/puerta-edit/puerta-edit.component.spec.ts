import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PuertaEditComponent } from './puerta-edit.component';

describe('PuertaEditComponent', () => {
  let component: PuertaEditComponent;
  let fixture: ComponentFixture<PuertaEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PuertaEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PuertaEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

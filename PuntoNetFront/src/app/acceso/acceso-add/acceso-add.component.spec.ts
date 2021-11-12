import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccesoAddComponent } from './acceso-add.component';

describe('AccesoAddComponent', () => {
  let component: AccesoAddComponent;
  let fixture: ComponentFixture<AccesoAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccesoAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccesoAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

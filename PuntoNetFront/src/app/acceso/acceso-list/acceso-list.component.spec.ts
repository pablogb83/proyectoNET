import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccesoListComponent } from './acceso-list.component';

describe('AccesoListComponent', () => {
  let component: AccesoListComponent;
  let fixture: ComponentFixture<AccesoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccesoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccesoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

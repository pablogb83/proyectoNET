import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VisitanteHomeComponent } from './visitante-home.component';

describe('VisitanteHomeComponent', () => {
  let component: VisitanteHomeComponent;
  let fixture: ComponentFixture<VisitanteHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VisitanteHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VisitanteHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

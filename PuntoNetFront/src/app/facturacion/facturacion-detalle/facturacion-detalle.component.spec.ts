import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FacturacionDetalleComponent } from './facturacion-detalle.component';

describe('FacturacionDetalleComponent', () => {
  let component: FacturacionDetalleComponent;
  let fixture: ComponentFixture<FacturacionDetalleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FacturacionDetalleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FacturacionDetalleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

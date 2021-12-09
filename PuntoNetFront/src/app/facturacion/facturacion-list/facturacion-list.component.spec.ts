import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FacturacionListComponent } from './facturacion-list.component';

describe('FacturacionListComponent', () => {
  let component: FacturacionListComponent;
  let fixture: ComponentFixture<FacturacionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FacturacionListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FacturacionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

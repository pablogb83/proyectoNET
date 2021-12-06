import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductosEditPriceComponent } from './productos-edit-price.component';

describe('ProductosEditPriceComponent', () => {
  let component: ProductosEditPriceComponent;
  let fixture: ComponentFixture<ProductosEditPriceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductosEditPriceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductosEditPriceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

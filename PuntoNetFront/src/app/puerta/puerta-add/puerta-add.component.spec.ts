import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PuertaAddComponent } from './puerta-add.component';

describe('PuertaAddComponent', () => {
  let component: PuertaAddComponent;
  let fixture: ComponentFixture<PuertaAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PuertaAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PuertaAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

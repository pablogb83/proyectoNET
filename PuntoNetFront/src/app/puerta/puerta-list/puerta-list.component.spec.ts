import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PuertaListComponent } from './puerta-list.component';

describe('PuertaListComponent', () => {
  let component: PuertaListComponent;
  let fixture: ComponentFixture<PuertaListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PuertaListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PuertaListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

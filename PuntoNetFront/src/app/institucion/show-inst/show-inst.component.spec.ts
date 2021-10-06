import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowInstComponent } from './show-inst.component';

describe('ShowInstComponent', () => {
  let component: ShowInstComponent;
  let fixture: ComponentFixture<ShowInstComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowInstComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowInstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

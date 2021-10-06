import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditInstComponent } from './add-edit-inst.component';

describe('AddEditInstComponent', () => {
  let component: AddEditInstComponent;
  let fixture: ComponentFixture<AddEditInstComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditInstComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditInstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

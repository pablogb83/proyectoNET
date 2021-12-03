import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EdificiosEditComponent } from './edificios-edit.component';

describe('EdificiosEditComponent', () => {
  let component: EdificiosEditComponent;
  let fixture: ComponentFixture<EdificiosEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EdificiosEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EdificiosEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

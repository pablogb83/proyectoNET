import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EdificiosAddComponent } from './edificios-add.component';

describe('EdificiosAddComponent', () => {
  let component: EdificiosAddComponent;
  let fixture: ComponentFixture<EdificiosAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EdificiosAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EdificiosAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

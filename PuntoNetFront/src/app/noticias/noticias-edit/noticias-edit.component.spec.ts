import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticiasEditComponent } from './noticias-edit.component';

describe('NoticiasEditComponent', () => {
  let component: NoticiasEditComponent;
  let fixture: ComponentFixture<NoticiasEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticiasEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticiasEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

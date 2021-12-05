import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticiasAddComponent } from './noticias-add.component';

describe('NoticiasAddComponent', () => {
  let component: NoticiasAddComponent;
  let fixture: ComponentFixture<NoticiasAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NoticiasAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticiasAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

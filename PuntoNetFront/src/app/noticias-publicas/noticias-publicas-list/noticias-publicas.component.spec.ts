import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoticiasPublicasComponent } from './noticias-publicas.component';

describe('NoticiasPublicasComponent', () => {
  let component: NoticiasPublicasComponent;
  let fixture: ComponentFixture<NoticiasPublicasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NoticiasPublicasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NoticiasPublicasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
